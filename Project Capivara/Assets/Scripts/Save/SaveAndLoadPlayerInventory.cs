using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class SaveAndLoadPlayerInventory : MonoBehaviour
{
    #region
    [Header("Player Inventory")]
    public string[] items;
    public int[] quantity;
    public int maxSlots;
    public Item[] newItems;
    public float[] newHairColor;
    public Item[] itemsAvaible;
    private PlayerInventory pI;
    public Item[] playerItems;
    public Image[] testColor;
    public float r, g, b = 0;
    #endregion

    #region
    public Color hairColor;
    #endregion

    private void Start()
    {
        newHairColor = new float[3] {0, 0, 0};
        playerItems = new Item[maxSlots];
        pI = CallLoad();
        if (pI == null)
        {
            ResetOptions();
            pI = CallLoad();
        }
        items = pI.GetAllItems();
        quantity = pI.GetAllItemsQuantity();
        r = pI.getHairColor()[0];
        g = pI.getHairColor()[1];
        b = pI.getHairColor()[2];
        for (int i = 0; i < testColor.Length; i++)
        {
            testColor[i].color = new Color(r, g, b, 1);
        }
        //TO-DO: Passar os valores do Load Para outra classe que cuide de gerenciar o inventário
    }

    public Item[] GetInventoryItems()
    {
        for (int i = 0, j = items.Length; i < j; i++)
        {
            if (items[i] != null && items[i].Equals(itemsAvaible[i].name))
            {
                playerItems[i] = itemsAvaible[i];
                playerItems[i].quantity = quantity[i];
            }
        }
        return playerItems;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetNewInventory();
        }
    }
    
    public int GetInventoryQuantity(int index)
    {
        return quantity[index];
    }

    public void GetAllCalledItems()
    {
        GetInventoryItems();
        print(playerItems);
    }
    
    public void SetNewInventory()
    {
        print("P!");
        string[] temporalItems = new string[maxSlots];
        int[] temporalQuantity = new int[maxSlots];
        for (int i = 0, j = newItems.Length; i < j; i++)
        {
            if (newItems[i] != null)
            {
                temporalItems[i] = newItems[i].name;
                temporalQuantity[i] = newItems[i].quantity;
            }
        }
        items = temporalItems;
        quantity = temporalQuantity;
        print(newHairColor[0]);
        print(testColor[0].color.r);
        newHairColor[0] = testColor[0].color.r;
        newHairColor[1] = testColor[0].color.g;
        newHairColor[2] = testColor[0].color.b;
        
        CallSaveOptions(items, quantity, newHairColor, false);
    }

    public void ShowInventory()
    {
        print(items);
        print(quantity);
    }
    
    public PlayerInventory CallLoad()
    { 
        PlayerInventory playerInventory = LoadOptions();
        if (playerInventory == null)
        {
            return null;
        }
        return playerInventory;
    }

    public PlayerInventory CallSaveOptions(string[] items, int[] quantity, float[] hairColor, bool isNew)
    {
        PlayerInventory playerInventory = new PlayerInventory();

        playerInventory.SetAllItems(items);
        playerInventory.SetAllItemsQuantity(quantity);
        playerInventory.setHairColor(hairColor);
        
        if (!isNew)
        {
            SaveOptions(playerInventory, true);
            return playerInventory;
        }
        SaveOptions(playerInventory, false);
        return null;
    }

    void SaveOptions(PlayerInventory playerInventory, bool isNew)
    {
        BinaryFormatter bF = new BinaryFormatter();

        string path = Application.persistentDataPath;

        if (!isNew)
        {
            File.Delete(path + "/playerInventory.save");
        }

        FileStream file = File.Create(path + "/playerInventory.save");
        bF.Serialize(file, playerInventory);
        file.Close();
    }

    public PlayerInventory LoadOptions()
    {
        BinaryFormatter bF = new BinaryFormatter();
        FileStream file;
        string path = Application.persistentDataPath;

        if (File.Exists(path + "/playerInventory.save"))
        {
            file = File.Open(path + "/playerInventory.save", FileMode.Open);
            PlayerInventory playerInventory = (PlayerInventory)bF.Deserialize(file);
            file.Close();
            return playerInventory;
        }
        return null;
    }

    public StartPlayerInventory ResetOptions()
    {
        StartPlayerInventory startPlayerInventory = new StartPlayerInventory();
        CallSaveOptions(startPlayerInventory.GetAllItems(), startPlayerInventory.GetAllItemsQuantity(),
            startPlayerInventory.GetHairColor(), true);
        return startPlayerInventory;
    }
}
