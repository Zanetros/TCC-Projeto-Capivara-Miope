using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShopPanel : ShopItemPanel
{
    public override void Show()
    {
        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
        {
            if (inventory.slots[i].item == null)
            {
                buttons[i].Clean();
            }
            else
            {
                if (inventory.slots[i].item != null && inventory.slots[i].item.juice)
                {
                    buttons[i].Set(inventory.slots[i], i);
                } 

                else
                {
                    buttons[i].Clean();
                }
            }
        }

    }

    public override void OnClick(int id)
    {
        GameManager.instance.dragAndDropController.OnClick(inventory.slots[id]);
        Show();
    }
}
