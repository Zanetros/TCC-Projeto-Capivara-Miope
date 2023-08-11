using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonsController : CraftingButtonsControls
{
    [SerializeField] PlayerShopPanel playerShopPanel;
    [SerializeField] private PlayerShopHandler playerShopHandler;
    public ItemSlot itemSlot;
    
    public virtual void SetIndex(int index)
    {
        myIndex = index;
    }

    public void GetItem()
    {
        playerShopHandler.ChooseJuice(itemSlot);
    }
    
    public void Set(ItemSlot slot, int index)
    {
        gameObject.GetComponent<Button>().enabled = true;
        SetIndex(index);
        itemSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.sprite;
        text.gameObject.SetActive(true);
        text.text = slot.count.ToString();
    }

    public override void Clear()
    {
        myIndex = -1;
        gameObject.GetComponent<Button>().enabled = false;
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        if(text != null) text.gameObject.SetActive(false);
    }
    
}
