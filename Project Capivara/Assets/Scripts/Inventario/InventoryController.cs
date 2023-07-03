using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject statusPanel;
    [SerializeField] GameObject toolBar;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] CoinBag coinBag;
    [SerializeField] private GameManager gameManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (panel.activeInHierarchy == false)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
    }

    public void Open()
    {
        moneyText.text = coinBag.coinsQuantity.ToString();
        panel.SetActive(true);
        statusPanel.SetActive(true);
        toolBar.SetActive(false);
        gameManager.characterInteractController.enabled = false;
        gameManager.playerMovement.enabled = false;
    }

    public void Close()
    {
        panel.SetActive(false);
        statusPanel.SetActive(false);
        toolBar.SetActive(true);
        gameManager.characterInteractController.enabled = true;
        gameManager.playerMovement.enabled = true;
    }
}
