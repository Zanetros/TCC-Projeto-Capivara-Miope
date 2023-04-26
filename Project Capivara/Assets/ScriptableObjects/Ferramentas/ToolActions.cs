using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolActions : ScriptableObject
{      
    public virtual bool OnAply(Vector2 worldPoint)
    {
        Debug.LogWarning("is no working");
        return true;
    }

    public virtual bool OnAplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
    {
        Debug.LogWarning("tilemap is not applied");
        return true;
    }

    public virtual void OnItemUsed(Item usedItem, ItemContainer inventory)
    {

    }
}
