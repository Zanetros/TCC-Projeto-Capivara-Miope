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
    public ItemDragAndDropController dropController;

    public void SetItensInShop()
    {
        dialogos = shopItem[Random.Range(0, shopItem.Length)];
        dialogText.text = dialogos.shopText;
        juiceSprite.sprite = dialogos.sprite;      
    }

    public void SellJuice()
    {
        if (dropController.inPlace)
        {
            if (itemSlot.item.Name == dialogos.Name)
            {
                GameManager.instance.coinBag.AddCoins(dialogos.sellPrice * itemSlot.count);
            }

            else if (itemSlot.item.Name != dialogos.Name)
            {
                GameManager.instance.coinBag.AddCoins(dialogos.sellPrice / 2 * itemSlot.count);
            }
        }        
    }
}
