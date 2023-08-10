using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellJuiceInteract : Interactable
{
    public override void Interact(Character character)
    {
        GameManager.instance.playerShopController.ShowShop();
        GameManager.instance.playerShop.SetItensInShop();
    }
}
