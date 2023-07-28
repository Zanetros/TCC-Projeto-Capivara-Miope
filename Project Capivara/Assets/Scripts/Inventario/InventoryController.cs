using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject toolBar;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] CoinBag coinBag;
    [SerializeField] private GameManager gameManager;
    public ItemContainer itemContainer;
    public CropsContainer container;

    public void Start()
    {
        itemContainer.LoadInventory();
        Debug.Log("Inventario Carregado");
        container.LoadCrops();
    }

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

        if (Input.GetKeyDown(KeyCode.G))
        {
            itemContainer.SaveInventory();
            container.SaveCrops();
        }
    }

    public void Open()
    {
        moneyText.text = coinBag.coinsQuantity.ToString();
        panel.SetActive(true);
        toolBar.SetActive(false);
        gameManager.ControlCharacterControls(false, false);
    }

    public void Close()
    {
        panel.SetActive(false);
        toolBar.SetActive(true);
        gameManager.ControlCharacterControls(true, true);
    }
}
