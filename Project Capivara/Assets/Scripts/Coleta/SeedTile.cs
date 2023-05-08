using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool action/Seed Tile")]
public class SeedTile : ToolActions
{
    public override bool OnAplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {
        if (tileMapReadController.cropsManager.Check(gridPosition) == false)
        {
            return false;
            Debug.Log("B");
        }
        
        tileMapReadController.cropsManager.Seed(gridPosition, item.crop);
        Debug.Log("A");
        
        return true;
    }

    public override void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        inventory.Remove(usedItem);
    }
}
