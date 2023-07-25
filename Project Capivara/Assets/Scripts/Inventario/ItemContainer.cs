using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[Serializable]

public class ItemSlot
{
    public Item item;
    public int count;
    public int id;

    public void Copy(ItemSlot slot)
    {
        item = slot.item;
        count = slot.count;
        id = slot.id;
    }   

    public void Clear()
    {
        item = null;
        count = 0;
        id = 0;
    }

    public void Set(Item item, int count, int id)
    {
        this.item = item;
        this.count = count;
        this.id = id;
    }
}


[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    private ItemDataBaseObject database;
    public List<ItemSlot> slots;
    public List<ItemSlot> itensInGame;
    public bool isDirty;
    private int[,] itensToReturn = {{0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0},
    {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, { 0,0 }};
    private int c = 0;

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDataBaseObject)AssetDatabase.LoadAssetAtPath
            ("Assets/Resources/Database.asset", typeof(ItemDataBaseObject));
#else
        database = Resources.Load<ItemDataBaseObject>("Database");
#endif
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {

    }


    //adiciona itens no inventario
    public void Add(Item item , int count = 1)
    {
        isDirty = true;
      
        //adiciona itens stackaveis no invetario 
        if (item.stackable == true)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == item);
            if (itemSlot != null)
            {
                itemSlot.count += count;
            }
            //se nao tiver o item no inventario ainda, adiciona ele
            else
            {
                itemSlot = slots.Find(x => x.item == null);
                if (itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                    itemSlot.id = item.itemId;
                }
            }
        }
        else
        {
            //adicionar item nao stackaveis pro container
            ItemSlot itemSlot = slots.Find(x => x.item = null);
            if (itemSlot == null)
            {
                itemSlot.item = item;
                itemSlot.count = count;
            }
        }
    }

    public void Remove(Item itemRemove, int count = 1)
    {
        isDirty = true;
        
        if (itemRemove.stackable)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == itemRemove);
            if (itemSlot == null) { return; }
            
            itemSlot.count -= count;
            if (itemSlot.count <= 0)
            {
                itemSlot.Clear();
            }
        }

        else
        {
            while (count > 0)
            {
                count -= 1;

                ItemSlot itemSlot = slots.Find(x => x.item == itemRemove);
                if (itemSlot == null) { break; }
                itemSlot.Clear();
            }
        }
    }

    internal bool CheckFreeSpace()
    {
        for(int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == null)
            {
                return true;
            }
        }
        
        return false;
    }

    internal bool CheckItem(ItemSlot checkingItem)
    {
        ItemSlot itemSlot = slots.Find(x => x.item = checkingItem.item);

        if (itemSlot == null) { return false; }

        if (checkingItem.item.stackable) { return itemSlot.count > 0; }

        return true;
    }

    public void SaveInventory()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        binaryFormatter.Serialize(file, saveData);
        file.Close();
    }

    public void LoadInventory()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(binaryFormatter.Deserialize(file).ToString(), this);
            file.Close();
        }
    }
}
