using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Tool action/Place Object")]
public class PlaceObject : ToolActions
{
    public override bool OnAplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {
        tileMapReadController.objectManager.Place(item, gridPosition);
        return true;
    }
}
