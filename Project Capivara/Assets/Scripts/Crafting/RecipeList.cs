using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/RecipeList")]
public class RecipeList : ScriptableObject
{
    public List<CraftingRecipe> recipes;
    private Item itemToFind = null;
    
    internal bool CheckRecipe(Item checkingRecipeItem)
    {
        itemToFind = null;
        for (int i = 0; i < recipes.Count; i++)
        {
            if (recipes[i] != null && recipes[i].output.item.Equals(checkingRecipeItem))
            {
                itemToFind = checkingRecipeItem;
            }    
        }
        if (itemToFind == null)
        { return false; }
        
        return true;
    }

    public void AddNewRecipeToList(CraftingRecipe newRecipe)
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (recipes[i] == null)
            {
                recipes[i] = newRecipe;
                Debug.Log("Você descobriu uma nova receita: " + newRecipe.output.item);
                break;
            }
        }
    }
    
}
