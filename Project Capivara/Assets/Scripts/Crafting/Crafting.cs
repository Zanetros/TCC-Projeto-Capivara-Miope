using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;
    [SerializeField] GameObject craftingPanel;
    [SerializeField] GameObject newRecipesPanel;
    [SerializeField] GameObject knownRecipesPanel;
    [SerializeField] RecipeList allRecipesInGame;
    [SerializeField] RecipeList recipesKnownList;
    public void OpenCrafting()
    {
        GameManager.instance.ControlCharacterControls(false, false);
        craftingPanel.SetActive(true);
        newRecipesPanel.SetActive(false);
        knownRecipesPanel.SetActive(true);
    }

    public void RecipesToResearch()
    {
        newRecipesPanel.SetActive(true);
        knownRecipesPanel.SetActive(false);
    }

    public void KnowRecipes()
    {
        newRecipesPanel.SetActive(false);
        knownRecipesPanel.SetActive(true);
    }

    public void CloseCrafting()
    {
        GameManager.instance.ControlCharacterControls(true, true);
        craftingPanel.SetActive(false);
    }
    
    public void Craft(CraftingRecipe recipe)
    {
        if (inventory.CheckFreeSpace() == false)
        {
            Debug.LogError("Sem espaco no inventario para craftar");
            return;
        }
        
        for (int i = 0; i < recipe.elements.Count; i++)
        {
            if (inventory.CheckItem(recipe.elements[i]) == false)
            {
                Debug.LogError("sem itens necessarios no invetario");
                return;
            }
        }

        for (int i = 0; i < recipe.elements.Count; i++)
        {
            inventory.Remove(recipe.elements[i].item, recipe.elements[i].count);
        }

        inventory.Add(recipe.output.item, recipe.output.count);
    }
}
