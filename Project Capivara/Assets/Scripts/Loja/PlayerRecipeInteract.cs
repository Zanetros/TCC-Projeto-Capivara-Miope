using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecipeInteract : Interactable
{
       public override void Interact(Character character)
       {
           GameManager.instance.ControlCharacterControls(false, false);
           GameManager.instance.crafting.OpenCrafting();
       }
}
