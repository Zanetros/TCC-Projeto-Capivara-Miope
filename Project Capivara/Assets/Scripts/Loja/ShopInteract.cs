using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteract : Interactable
{
    public override void Interact(Character character)
    {
        GameManager.instance.shopController.OpenShop();
    }
}
