using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract : Interactable
{
    [SerializeField] GameObject chest;
    [SerializeField] bool open;
    
    public override void Interact(Character character)
    {
       if (open == false)
        {
            open = true;
        }
    }
}
