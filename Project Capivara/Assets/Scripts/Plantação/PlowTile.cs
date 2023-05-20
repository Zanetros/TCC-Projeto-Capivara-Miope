using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool action/Hoe")]
public class PlowTile : ToolActions
{
    public List<TileBase> canPlow;
    [SerializeField] AudioClip onPlowUsed;
    
    public override bool OnAplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {
        TileBase tileToPlow = tileMapReadController.GetTileBase(gridPosition);

        if (canPlow.Contains(tileToPlow) == false)
        {
            return false;
        }

        tileMapReadController.cropsManager.Plow(gridPosition);

        AudioManager.instance.Play(onPlowUsed);
        
        return true;
    }
}
