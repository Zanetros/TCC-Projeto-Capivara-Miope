using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool action/Hoe")]
public class PlowTile : ToolActions
{
    public List<TileBase> canPlow;
    
    public override bool OnAplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
    {
        TileBase tileToPlow = tileMapReadController.GetTileBase(gridPosition);

        if (canPlow.Contains(tileToPlow) == false)
        {
            return false;
        }

        tileMapReadController.cropsManager.Plow(gridPosition);
        
        return true;
    }
}
