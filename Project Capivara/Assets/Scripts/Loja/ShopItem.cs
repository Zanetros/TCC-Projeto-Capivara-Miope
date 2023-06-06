using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{

    public TextMeshProUGUI txtPrice;
    public Image imgSprite;
    
    private Item shopItem;
    public GameManager gameManager;
    
    public void GetItem(Item myItem)
    {
        gameObject.SetActive(true);
        shopItem = myItem;
        imgSprite.sprite = shopItem.sprite;
        txtPrice.text = "" + shopItem.sellPrice;
    }
    
    public void SendItem()
    {
        gameManager.shopController.SelectItem(shopItem);
    }

    public void ClearItem()
    {
        shopItem = null;
        gameObject.SetActive(false);
    }
    
}