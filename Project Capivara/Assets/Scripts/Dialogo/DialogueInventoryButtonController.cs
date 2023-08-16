using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueInventoryButtonController : CraftingButtonsControls
{
    public DialogueSystem dialogueSystem;
    private ItemSlot myItem;
    public TextMeshProUGUI nameText;
    public override void Set(ItemSlot slot, int index, bool selectedAsIngredient)
    {
        myItem = slot;
        gameObject.GetComponent<Button>().enabled = true;
        SetIndex(index);
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.sprite;
        if (nameText != null)
        {
            nameText.text = myItem.item.Name;
        }
        if (text != null)
        {
            text.gameObject.SetActive(true);
            text.text = myItem.count.ToString();
        }
    }

    public void SelectItem()
    {
        dialogueSystem.GetItemToGive(myItem, myIndex);
    }

}
