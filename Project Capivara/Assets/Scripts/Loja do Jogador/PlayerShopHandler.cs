using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerShopHandler : MonoBehaviour
{
    public Item[] shopItem;
    public TextMeshProUGUI dialogText;
    public Image juiceSprite;
    Item dialogos;
    public ItemSlot itemSlot;
    public PlayerShopPanel playerShopPanel;
    public PlayerShopButton sellJuiceButtonController;

    public void SetItensInShop()
    {
        dialogos = shopItem[Random.Range(0, shopItem.Length)];
        dialogText.text = dialogos.shopText;
        juiceSprite.sprite = dialogos.sprite;      
    }

    public void SellJuice()
    {
        if (itemSlot != null)
        {
            if (itemSlot.item.Name == dialogos.Name)
            {
                GameManager.instance.coinBag.AddCoins(dialogos.sellPrice);
            }
            else if (itemSlot.item.Name != dialogos.Name)
            {
                GameManager.instance.coinBag.AddCoins(dialogos.sellPrice / 2);
            }
            playerShopPanel.RemoveSoldItemFromInventory(itemSlot);
            itemSlot = null;
        }        
    }

    public void ChooseJuice(ItemSlot item)
    {
        if (item != null)
        {
            itemSlot = item;
            sellJuiceButtonController.Set(item, item.id);
        }
    }
    
}
