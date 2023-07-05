using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingPanel : MonoBehaviour
{
    public ItemContainer inventory;
    public RecipeList recipeList;
    public Crafting crafting;
    public CraftingButtonsControls ingredientsUsedButtons1;
    public CraftingButtonsControls ingredientsUsedButtons2;
    public List<CraftingButtonsControls> buttons;
    private int ingredientCount = 0;
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
                buttons[ingredientCount].Set(inventory.slots[i], i, false);
               ingredientCount++;   
            }
           
        }
        ingredientCount = 0;
    }

    public void GetIngredient(int index)
    {
        if (ingredientsUsedButtons1.myIndex.Equals(-1))
        {
            ingredientsUsedButtons1.Set(inventory.slots[index], index, true);
            return;
        }
        if (ingredientsUsedButtons2.myIndex.Equals(-1))
        {
            ingredientsUsedButtons2.Set(inventory.slots[index], index, true);
        }
    }

    public void TryRecipe()
    {
        if (ingredientsUsedButtons1.myIndex != -1 && ingredientsUsedButtons2.myIndex != -1)
        {
            for (int i = 0; i < recipeList.recipes.Count; i++)
            {
                //Receita em ordem
                if (recipeList.recipes[i].elements[0].item.Equals(inventory.slots[ingredientsUsedButtons1.myIndex].item))
                {
                    if (recipeList.recipes[i].elements[1].item.Equals(inventory.slots[ingredientsUsedButtons2.myIndex].item))
                    {
                        crafting.Craft(recipeList.recipes[i]);
                        Show();
                    }
                }
                else //Mesma Receita, mas inversa
                if (recipeList.recipes[i].elements[0].item.Equals(inventory.slots[ingredientsUsedButtons2.myIndex].item))
                {
                    if (recipeList.recipes[i].elements[1].item.Equals(inventory.slots[ingredientsUsedButtons1.myIndex].item))
                    {
                        crafting.Craft(recipeList.recipes[i]);
                        Show();
                    }
                }
            }
        }
        ingredientsUsedButtons1.Clear();
        ingredientsUsedButtons2.Clear();
    }

}
