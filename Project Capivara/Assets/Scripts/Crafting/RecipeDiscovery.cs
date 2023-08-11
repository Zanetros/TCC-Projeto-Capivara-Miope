using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeDiscovery : MonoBehaviour
{
    public ItemContainer inventory;
    public List<CraftingButtonsControls> buttons;
    
    public GameObject recipePanel;

    public void Start()
    {
        Show();
    }

    public void Show()
    {
        foreach (CraftingButtonsControls cbt in buttons)
        {
            cbt.Clear();
        }
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i].item != null && inventory.slots[i].item.usableInRecipe)
            {
                buttons[i].Set(inventory.slots[i], i, false);           
            }
        }
        
    }

    public void OpenDiscoveryPanel()
    {
        GameManager.instance.ControlCharacterControls(false, false);
        recipePanel.SetActive(true);
    }

    public void CloseDiscoveryPanel()
    {
        GameManager.instance.ControlCharacterControls(true, true);
        recipePanel.SetActive(false);
    }
}
