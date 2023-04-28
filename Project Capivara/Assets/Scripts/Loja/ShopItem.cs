using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{

    public TextMeshProUGUI txtPrice;
    public Image imgSprite;
    
    public Item shopItem;
    public GameManager gameManager;


    void Start()
    {
        imgSprite.sprite = shopItem.sprite;
        txtPrice.text = "" + shopItem.sellPrice;
    }
    
    public void SendItem()
    {
        gameManager.shopController.SelectItem(shopItem);
    }
    
}