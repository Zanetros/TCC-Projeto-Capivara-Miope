using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShopPanel : ShopItemPanel
{
    private int juiceCount = 0;
    public List<ShopButtonsController> shopButtons;
    public override void Show()
    { 
        foreach (ShopButtonsController shopButtonsController in shopButtons)
        {
            shopButtonsController.Clear();
        }
        for (int i = 0; i < inventory.slots.Count; i++)
        { 
            if (inventory.slots[i].item != null && inventory.slots[i].item.juice)
            { 
                shopButtons[juiceCount].Set(inventory.slots[i], i);
               juiceCount++; 
            }
        }
        juiceCount = 0;
    }

    public void RemoveSoldItemFromInventory(ItemSlot itemSlot)
    {
        inventory.Remove(itemSlot.item, 1);
        Show();
    }
}
