using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingButtonsControls : MonoBehaviour
{
    [SerializeField] protected Image icon;
    [SerializeField] protected TextMeshProUGUI text;
    [SerializeField] private Image highlight;
    [SerializeField] private CraftingPanel craftingPanel;
    public int myIndex = -1;
    private bool selected = false;

    public virtual void SetIndex(int index)
    {
        myIndex = index;
    }
    
    public virtual void Set(ItemSlot slot, int index, bool selectedAsIngredient)
    {
        gameObject.GetComponent<Button>().enabled = true;
        SetIndex(index);
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.sprite;
        if (!selectedAsIngredient)
        {
            text.gameObject.SetActive(true);
            text.text = slot.count.ToString();   
        }
    }

    public virtual void Clear()
    {
        myIndex = -1;
        gameObject.GetComponent<Button>().enabled = false;
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        if(text != null) text.gameObject.SetActive(false);
    }

    public void SelectIngredient()
    {
        craftingPanel.GetIngredient(myIndex);
    }
}
