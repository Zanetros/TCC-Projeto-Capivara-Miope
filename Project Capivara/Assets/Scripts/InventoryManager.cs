using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryCanvas;
    private bool isInventoryOpen = false;
    public InventorySlotManager[] inventorySlots;
    public InventorySlotManager[] quickBarSlots;
    public Item[] _items;

    public SaveAndLoadPlayerInventory save;

    void Start()
    {
        PopulateQuickBar();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }    
    }

    private void ShowInventory()
    {
        if (isInventoryOpen)
        {
            inventoryCanvas.SetActive(false);
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                inventorySlots[i].gameObject.SetActive(false);
            }
        }
        else
        {
            inventoryCanvas.SetActive(true);
            PopulateInventory();
        }
        isInventoryOpen = !isInventoryOpen;
    }

    private void PopulateInventory()
    {
        _items = save.GetInventoryItems();
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] != null)
            {
                inventorySlots[i].Populate(_items[i]);
            }
        }
    }

    private void PopulateQuickBar()
    {
        _items = save.GetInventoryItems();
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] != null)
            {
                if (i < quickBarSlots.Length)
                {
                    quickBarSlots[i].Populate(_items[i]);
                }
            }
        }
    }
}
