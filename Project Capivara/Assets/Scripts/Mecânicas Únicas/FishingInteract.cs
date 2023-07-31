using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingInteract : Interactable
{
    public GameObject signal;
    public override void Interact(Character character)
    {
        GameManager.instance.fishingController.Initialize(signal);
    }
}
