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

    public void SetItensInShop()
    {
        var Dialogos = shopItem[Random.Range(0, shopItem.Length)];
        dialogText.text = Dialogos.shopText;
        juiceSprite.sprite = Dialogos.sprite;      
    }

}
