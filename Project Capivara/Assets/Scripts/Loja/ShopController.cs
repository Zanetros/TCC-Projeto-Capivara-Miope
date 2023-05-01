using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{

    public GameObject shopPanel;
    public GameObject confirmPanel;
    public TextMeshProUGUI txtQuantity;
    public TextMeshProUGUI txtItemName;
    public TextMeshProUGUI txtTotalPrice;
    public TextMeshProUGUI txtCoinsInBag;
    public Image imgItemConfirm;
    public ItemContainer inventory;
    
    private Item selectedItem;   
    private int quantityToBuy;
    private int totalPrice;

    private bool open = false;
    public GameManager gameManager;
    
    public void OpenShop()
    {
        gameManager.playerMovement.enabled = false;
        gameManager.characterInteractController.enabled = false;
        ResetShop();
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        gameManager.playerMovement.enabled = true;
        gameManager.characterInteractController.enabled = true;
        ResetShop();
        shopPanel.SetActive(false);
    }

    private void ResetShop()
    {
        selectedItem = null;
        quantityToBuy = 1;
        totalPrice = 0;
    }
    
    public void SelectItem(Item itemToBuy)
    {
        confirmPanel.SetActive(true);
        selectedItem = itemToBuy;
        txtItemName.text = itemToBuy.Name;
        imgItemConfirm.sprite = itemToBuy.sprite;
        CalculatePrice();
    }

    public void CancelTrade()
    {
        confirmPanel.SetActive(false);
        txtItemName.text = "";
        ResetShop();
    }

    public void AddQuantity()
    {
        if ((quantityToBuy + 1) * selectedItem.sellPrice <= gameManager.coinBag.coinsQuantity)
        {
            quantityToBuy++;
            CalculatePrice();
        }
    }
    
    public void MinusQuantity()
    {
        if (quantityToBuy > 1)
        {
            quantityToBuy--;
            CalculatePrice();   
        }
    }
    
    private void CalculatePrice()
    {
        txtQuantity.text = "x" + quantityToBuy;
        totalPrice = quantityToBuy * selectedItem.sellPrice;
        txtTotalPrice.text = totalPrice + "";
    }

    public void Buy()
    {
        if (gameManager.coinBag.coinsQuantity >= totalPrice)
        {
            gameManager.coinBag.RemoveCoins(totalPrice);
            txtCoinsInBag.text = "" + gameManager.coinBag.coinsQuantity;
            confirmPanel.SetActive(false);
            //O item a ser comprado é o "selectedItem" e sua quantidade é a "quantityToBuy"
            //Nessa linha seria a parte de adicionar o item no inventário do jogador de acordo com a quantidade comprada
            Debug.Log("Voce comprou " + quantityToBuy +  " itens");
            ItemBought(selectedItem, quantityToBuy);
            ResetShop();
        }
        else
        {
            print("Voce nao tem dinheiro o suficiente");
        }
    }

    public void ItemBought(Item itemBought, int count)
    {
        selectedItem = itemBought;
        quantityToBuy = count;
        inventory.Add(itemBought, count);
        
        
    }  
}
