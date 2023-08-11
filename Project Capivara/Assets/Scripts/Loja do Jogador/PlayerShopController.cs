using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShopController : MonoBehaviour
{
    public GameObject playerShopPanel;

    public void ShowShop()
    {
        GameManager.instance.ControlCharacterControls(false, false);
        playerShopPanel.SetActive(true);
    }
   
   
    public void CloseShopPanel()
    {
        GameManager.instance.ControlCharacterControls(true, true);
        playerShopPanel.SetActive(false);
    }
}
