using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeDiscoveryInteract : Interactable
{
    public override void Interact(Character character)
    {
        GameManager.instance.recipeDiscovery.OpenDiscoveryPanel();
    }
}
