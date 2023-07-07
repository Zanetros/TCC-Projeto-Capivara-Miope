using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPanel : MonoBehaviour
{
    [Header("Lists & Inventory")]
    #region
    public ItemContainer inventory;
    public RecipeList recipeList;
    public RecipeList knownRecipes;
    public Crafting crafting;
    #endregion

    [Header("Manual Crafting")]
    #region
    public CraftingButtonsControls ingredientsUsedButtons1;
    public CraftingButtonsControls ingredientsUsedButtons2;
    public List<CraftingButtonsControls> buttons;
    private int ingredientCount = 0;
    public int firstItemShown = -1;
    public int lastItemShown = -1;
    private int c = 0;
    #endregion
    
    [Header("Recipes Known")]
    #region
    public CraftingButtonsControls recipeSelectedButton;
    private CraftingRecipe recipeToTry = null;
    public List<RecipeButtonsControl> recipeButtons;
    public TextMeshProUGUI txtRecipeName;
    public GameObject btnPlus;
    public GameObject btnMinus;
    public TextMeshProUGUI txtQuantityToCreate;
    public Image imgIngredient1;
    public Image imgIngredient2;
    public TextMeshProUGUI txtNumberIngredient1;
    public TextMeshProUGUI txtNumberIngredient2;
    public TextMeshProUGUI txtPageNumber;
    private int actualQuantityToCreate = 0;
    private bool canCreate = false;
    private int page = 0;
    private int o;
    private int p;
    #endregion
    
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

    public void ShowRecipesKnown()
    {
        foreach (RecipeButtonsControl rBc in recipeButtons)
        {
            rBc.Disable();
            //rBc.Clear();
        }
        
        firstItemShown = 6 * page;
        lastItemShown = firstItemShown + 6;
        if (lastItemShown > recipeList.recipes.Count)
        {
            lastItemShown = recipeList.recipes.Count;
        }
        for (int i = firstItemShown; i < recipeButtons.Count; i++)
        {
            if (i <= lastItemShown + 1)
            {
                if (knownRecipes.recipes.Contains(recipeList.recipes[i]))
                {
                    recipeButtons[c].Set(recipeList.recipes[i], i);
                }
                else
                {
                    recipeButtons[c].Clear();
                }
            }
            else
            {
                recipeButtons[i].Disable();
            }
            c++;
        }
        c = 0;
    }

    public void ChangePage(int c)
    {
        if (firstItemShown + c >= 0 && lastItemShown + c <= recipeList.recipes.Count)
        {
            page = page + c;
            ShowRecipesKnown();
            txtPageNumber.text = (page + 1) + "/" + (Mathf.Round(recipeList.recipes.Count/8) + 1);
        }
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

    public void GetRecipe(CraftingRecipe recipe)
    {
        recipeSelectedButton.GetComponent<Button>().enabled = true;
        recipeToTry = recipe;
        txtRecipeName.gameObject.SetActive(true);
        txtRecipeName.text = recipeToTry.output.item.Name;
        imgIngredient1.gameObject.SetActive(true);
        imgIngredient2.gameObject.SetActive(true);
        imgIngredient1.sprite = recipeToTry.elements[0].item.sprite;
        imgIngredient2.sprite = recipeToTry.elements[1].item.sprite;
        txtNumberIngredient1.gameObject.SetActive(true);
        txtNumberIngredient2.gameObject.SetActive(true);
        if (inventory.slots.Contains(recipeToTry.elements[0]))
        {
            o = inventory.slots.IndexOf(recipeToTry.elements[0]);
            txtNumberIngredient1.text = inventory.slots[o].count.ToString();
        }
        else
        if (inventory.slots.Contains(recipeToTry.elements[1]))
        {
            o = inventory.slots.IndexOf(recipeToTry.elements[1]);
            txtNumberIngredient1.text = inventory.slots[o].count.ToString();
        }
        if(!inventory.slots.Contains(recipeToTry.elements[0]) && !inventory.slots.Contains(recipeToTry.elements[1]))
        {
            txtNumberIngredient1.text = 0.ToString();
            canCreate = false;
        }
        
        
        if (inventory.slots.Contains(recipeToTry.elements[0]))
        {
            p = inventory.slots.IndexOf(recipeToTry.elements[0]);
            txtNumberIngredient2.text = inventory.slots[p].count.ToString();
        }
        else
        if (inventory.slots.Contains(recipeToTry.elements[1]))
        {
            p = inventory.slots.IndexOf(recipeToTry.elements[1]);
            txtNumberIngredient2.text = inventory.slots[p].count.ToString();
        }
        if(!inventory.slots.Contains(recipeToTry.elements[0]) && !inventory.slots.Contains(recipeToTry.elements[1]))
        {
            txtNumberIngredient2.text = 0.ToString();
            canCreate = false;
        }
        VerifyIfCanCreate(canCreate);
    }

    private void VerifyIfCanCreate(bool c)
    {
        if (c)
        {
            btnPlus.SetActive(true);
            btnMinus.SetActive(true);
            txtQuantityToCreate.gameObject.SetActive(true);
            txtQuantityToCreate.text = 1.ToString();
        }
        else
        {
            btnPlus.SetActive(false);
            btnMinus.SetActive(false);
            txtQuantityToCreate.gameObject.SetActive(false);
        }
        canCreate = false;
    }
    
    public int CalculateQuantity(int addValue)
    {
        if (actualQuantityToCreate + addValue <= inventory.slots[o].count &&
            actualQuantityToCreate + addValue <= inventory.slots[p].count && actualQuantityToCreate + addValue > 0)
        {
            actualQuantityToCreate += addValue;
        }
        return actualQuantityToCreate;
    }
    
    public void DeselectRecipe()
    {
        recipeToTry = null;
        txtRecipeName.gameObject.SetActive(false);
        btnPlus.SetActive(false);
        btnMinus.SetActive(false);
        txtQuantityToCreate.gameObject.SetActive(false);
        imgIngredient1.gameObject.SetActive(false);
        imgIngredient2.gameObject.SetActive(false);
        txtNumberIngredient1.gameObject.SetActive(false);
        txtNumberIngredient2.gameObject.SetActive(false);
        recipeSelectedButton.Clear();
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
