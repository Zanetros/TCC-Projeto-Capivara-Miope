using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinBag : MonoBehaviour
{
    public TextMeshProUGUI txtCoins;
    public int coinsQuantity;

    void Start()
    {
        UpdateCoinsQuantity();
    }
    
    public void AddCoins(int coins)
    {
        coinsQuantity += coins;
        UpdateCoinsQuantity();
    }
    
    public void RemoveCoins(int coins)
    {
        coinsQuantity -= coins;
        UpdateCoinsQuantity();
    }

    void UpdateCoinsQuantity()
    {
        txtCoins.text = "" + coinsQuantity;
    }
    
}
