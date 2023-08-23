using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System.IO;

public class SaveTiles : MonoBehaviour
{
    public static SaveTiles instance;
    List<CustomTile> tiles = new List<CustomTile>();
    
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) SaveTile();
        if (Input.GetKeyDown(KeyCode.RightAlt)) LoadTile();
    }

    public Tilemap tilemap;

    public void SaveTile()
    {
        BoundsInt bounds = tilemap.cellBounds;

        LevelData levelData = new LevelData();

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                TileBase temp = tilemap.GetTile(new Vector3Int(x, y, 0));
                CustomTile temptile = tiles.Find(t => t.tile == temp);

                if (temptile != null)
                {
                    levelData.tiles.Add(temptile.id);
                    levelData.pos.Add(new Vector3Int(x, y, 0));
                }
            }
        }

        string json = JsonUtility.ToJson(levelData, true);
        File.WriteAllText(Application.dataPath + "/testLevel.json", json);
    }

    public void LoadTile()
    {
        string json = File.ReadAllText(Application.dataPath + "/testLevel.json");
        LevelData data = JsonUtility.FromJson<LevelData>(json);

        tilemap.ClearAllTiles();

        for (int i = 0; i < data.pos.Count; i++)
        {
            tilemap.SetTile(data.pos[i], tiles.Find(t => t.name == data.tiles[i]).tile);
        }
    }
}

public class LevelData
{
    public List<string> tiles = new List<string>();
    public List<Vector3Int> pos = new List<Vector3Int>();
}
