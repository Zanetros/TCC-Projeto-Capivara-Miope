using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeButtonsControl : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image highlight;
    [SerializeField] CraftingPanel craftingPanel;
    public Sprite unkownSprite;
    public int myIndex = -1;
    private bool selected = false;

    public void SetIndex(int index)
    {
        myIndex = index;
    }
    
    public void Set(CraftingRecipe recipe, int index)
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<Button>().enabled = true;
        SetIndex(index);
        icon.gameObject.SetActive(true);
        icon.sprite = recipe.output.item.sprite;
        nameText.text = recipe.output.item.Name;
    }

    public void SelectRecipe()
    {
        if(myIndex != -1)
        {
            craftingPanel.GetRecipe(craftingPanel.recipeList.recipes[myIndex]);
        }
    }
    
    public void Clear()
    {
        gameObject.SetActive(true);
        myIndex = -1;
        gameObject.GetComponent<Button>().enabled = false;
        icon.sprite = unkownSprite;
        nameText.text = "???";
    }

    public void Disable()
    {
        myIndex = -1;
        gameObject.SetActive(false);
    }
}
