using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;
    [SerializeField] GameObject craftingPanelP;
    [SerializeField] GameObject newRecipesPanel;
    [SerializeField] GameObject knownRecipesPanel;
    [SerializeField] RecipeList allRecipesInGame;
    [SerializeField] RecipeList recipesKnownList;
    [SerializeField] CraftingPanel craftingInventoryPanel;
    public void OpenCrafting()
    {
        GameManager.instance.ControlCharacterControls(false, false);
        craftingPanelP.SetActive(true);
        RecipesToResearch();
    }

    public void RecipesToResearch()
    {
        newRecipesPanel.SetActive(true);
        knownRecipesPanel.SetActive(false);
        craftingInventoryPanel.Show();
    }

    public RecipeList LoadKnownRecipes(int[] recipes)
    {
        foreach (int recipe in recipes)
        {
            
        }
        return recipesKnownList;
    }
    
    public void KnowRecipes()
    {
        newRecipesPanel.SetActive(false);
        knownRecipesPanel.SetActive(true);
        craftingInventoryPanel.ShowRecipesKnown();
    }

    public void CloseCrafting()
    {
        GameManager.instance.ControlCharacterControls(true, true);
        craftingPanelP.SetActive(false);
    }
    
    public void Craft(CraftingRecipe recipe)
    {
        if (!inventory.CheckFreeSpace())
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
        
        //Se possui os ingredientes no inventário e um espaço livre lá, retira os ingredientes do inventário
        for (int i = 0; i < recipe.elements.Count; i++)
        {
            inventory.Remove(recipe.elements[i].item, recipe.elements[i].count);
        }
        
        inventory.Add(recipe.output.item, recipe.output.count);
        print("Você fez : " + recipe.output.item.Name);
    }
    
    public void CraftForQuantity(CraftingRecipe recipe, int quantity)
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
            inventory.Remove(recipe.elements[i].item, recipe.elements[i].count * quantity);
        }
        
        inventory.Add(recipe.output.item, recipe.output.count * quantity);
        print("Você fez : " + quantity + " " + recipe.output.item.Name);
    }

    public void VerifyIfItsKnownRecipe(CraftingRecipe possibleNewRecipe)
    {
        if (!recipesKnownList.CheckRecipe(possibleNewRecipe.output.item))
        {
            print("!");
            recipesKnownList.AddNewRecipeToList(possibleNewRecipe);
        }
    }
    
}