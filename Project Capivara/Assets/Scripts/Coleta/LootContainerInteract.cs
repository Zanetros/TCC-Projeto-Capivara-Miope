using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract : Interactable
{
    [SerializeField] GameObject closedChest;
    [SerializeField] GameObject openedChest;
    [SerializeField] bool open = false;
    
    public override void Interact(Character character)
    {
        open = !open;
        closedChest.SetActive(!closedChest.activeSelf);
        openedChest.SetActive(!openedChest.activeSelf);
    }
}