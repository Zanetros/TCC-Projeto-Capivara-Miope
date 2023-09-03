using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool action/Eat")]
public class EatAction : ToolActions
{
    public override void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        inventory.Remove(usedItem);
        GameManager.instance.playerStamina.GiveStamina(10);
        Debug.Log("comeu");
    }
}
