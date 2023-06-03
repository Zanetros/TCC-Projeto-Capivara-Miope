using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool action/ Harvest")]
public class OnTilePickUpAction : ToolActions
{
    public override bool OnAplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {
        tileMapReadController.cropsManager.PickUp(gridPosition);

        tileMapReadController.objectManager.PickUp(gridPosition);
        
        return true;
    }
}
