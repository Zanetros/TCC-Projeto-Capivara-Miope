using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract : Interactable
{
    [SerializeField] GameObject closedChest;
    [SerializeField] GameObject openedChest;
    [SerializeField] bool open;
    [SerializeField] ItemContainer itemContainer;
    
    public override void Interact(Character character)
    {
        if (open == false)
        {
            Open(character);
        }

        else
        {
            Close(character);
        }      
    }

    public void Open(Character character)
    {
        open = true;
        closedChest.SetActive(false);
        openedChest.SetActive(true);
        character.GetComponent<ItemContainerInteractController>().Open(itemContainer, transform);
    }

    public void Close(Character character)
    {
        open = false;
        closedChest.SetActive(true);
        openedChest.SetActive(false);

        character.GetComponent<ItemContainerInteractController>().Close();
    }
}