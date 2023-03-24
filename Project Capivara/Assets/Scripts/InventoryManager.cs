using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryCanvas;
    private bool isInventoryOpen = false;
    public InventorySlotManager[] inventorySlots;
    private List<Item> _items;

    public SaveAndLoadPlayerInventory save;
    
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
        for (int i = 0; i < _items.Count; i++)
        {
            inventorySlots[i].Populate(_items[i]);
        }
    }
}
