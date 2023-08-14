using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueInventoryButtonController : CraftingButtonsControls
{
    public DialogueSystem dialogueSystem;
    public override void Set(ItemSlot slot, int index, bool selectedAsIngredient)
    {
        gameObject.GetComponent<Button>().enabled = true;
        SetIndex(index);
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.sprite;
    }
    
    
    public void SelectItem()
    {
        
    }
}
