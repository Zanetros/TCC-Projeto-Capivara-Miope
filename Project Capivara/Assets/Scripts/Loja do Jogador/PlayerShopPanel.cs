using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShopPanel : ShopItemPanel
{
    private int juiceCount = 0;
    public override void Show()
    { 
        foreach (PlayerShopButton playerShopB in buttons)
        {
            playerShopB.Clean();
        }
        for (int i = 0; i < inventory.slots.Count; i++)
        { 
            if (inventory.slots[i].item != null && inventory.slots[i].item.juice)
            { 
               buttons[juiceCount].Set(inventory.slots[i], i);
               juiceCount++; 
            }
        }
        juiceCount = 0;
    }

    public override void OnClick(int id)
    {
        GameManager.instance.dragAndDropController.OnClick(inventory.slots[id]);
        Show();
    }
}
