using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Database")]
public class ItemDataBaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    //public Item[] Items;
    public ItemSlot[] slots;
    public Dictionary<ItemSlot, int> GetId = new Dictionary<ItemSlot, int>();
    public Dictionary<int, ItemSlot> GetItem = new Dictionary<int, ItemSlot>();
    //public Dictionary<Item, int> GetId = new Dictionary<Item, int>();
    //public Dictionary<int, Item> GetItem = new Dictionary<int, Item>();

    public void OnAfterDeserialize()
    {/*
        GetId = new Dictionary<Item, int>();
        GetItem = new Dictionary<int, Item>();
        for (int i = 0; i < Items.Length; i++)
        {
            GetId.Add(Items[i], i);
            GetItem.Add(i, Items[i]);
        }*/
        GetId = new Dictionary<ItemSlot, int>();
        GetItem = new Dictionary<int, ItemSlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            GetId.Add(slots[i], i);
            GetItem.Add(i, slots[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
}
