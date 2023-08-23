using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class TileMapSaveHandler : MonoBehaviour
{
    Dictionary<string, Tilemap> tilemaps = new Dictionary<string, Tilemap>();
    [SerializeField] BoundsInt bounds;
    [SerializeField] string filename = "tilemapData.json";

    private void Start()
    {
        InitTilemap();
    }

    private void InitTilemap()
    {
        Tilemap[] maps = FindObjectsOfType<Tilemap>();

        foreach (var map in maps)
        {
            tilemaps.Add(map.name, map);
        }
    }

    public void onSave()
    {
        List<TilemapData> data = new List<TilemapData>();

        foreach (var mapObj in tilemaps)
        {
            TilemapData mapData = new TilemapData();
            mapData.key = mapObj.Key;

            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    Vector3Int pos = new Vector3Int(x, y, 0);
                    TileBase tile = mapObj.Value.GetTile(pos);

                    if (tile != null)
                    {
                        TileInfo ti = new TileInfo(tile, pos);
                        mapData.tiles.Add(ti);
                    }
                }                
            }
            data.Add(mapData);
        }

        FileHandler.SaveToJSON<TilemapData>(data, filename);
    }

    public void onLoad()
    {
        List<TilemapData> data = FileHandler.ReadListFromJSON<TilemapData>(filename);

        foreach (var mapData in data)
        {
            if (!tilemaps.ContainsKey(mapData.key))
            {
                Debug.LogError("nothing here");
            }

            var map = tilemaps[mapData.key];

            map.ClearAllTiles();

            if (mapData.tiles != null && mapData.tiles.Count > 0)
            {
                foreach  (TileInfo tile in mapData.tiles)
                {
                    map.SetTile(tile.position, tile.tile);
                }
            }
        }
    }

    [Serializable]
    public class TilemapData
    {
        public string key;
        public List<TileInfo> tiles = new List<TileInfo>();
    }

    [Serializable]
    public class TileInfo
    {
        public TileBase tile;
        public Vector3Int position;

        public TileInfo (TileBase tile, Vector3Int pos)
        {
            this.tile = tile;
            position = pos;
        }
    }
}
