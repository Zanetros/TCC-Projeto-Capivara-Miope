using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{
    public ItemContainer playerInv;

    public bool EatItem()
    {        
        Item item = GameManager.instance.toolBarController.GetItem;
        if (item == null) { return false; }
        if (item.onEat == null) { return false; }

        item.onEat.OnItemUsed(item, playerInv);

        return false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EatItem();
        }        
    }
}
