using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject statusPanel;
    [SerializeField] GameObject toolBar;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            panel.SetActive(!panel.activeInHierarchy);
            statusPanel.SetActive(!statusPanel.activeInHierarchy);
            toolBar.SetActive(!toolBar.activeInHierarchy);
        }
    }
}
