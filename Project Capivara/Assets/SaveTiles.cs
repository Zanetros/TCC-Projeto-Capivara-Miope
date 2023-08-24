using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System.IO;

public class SaveTiles : MonoBehaviour
{
    public static SaveTiles instance;
    public Tilemap tilemap;
    [SerializeField] List<CustomTile> tiles = new List<CustomTile>();
    
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

    

    public void SaveTile()
    {
        BoundsInt bounds = tilemap.cellBounds;

        Debug.Log("TILE SALVO");

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
                    levelData.posX.Add(x);
                    levelData.posY.Add(y);
                }
            }
        }

        string json = JsonUtility.ToJson(levelData, true);
        File.WriteAllText(Application.dataPath + "/testLevel.json", json);
    }

    public void LoadTile()
    {
        Debug.Log("TILE CARREGADO");

        string json = File.ReadAllText(Application.dataPath + "/testLevel.json");
        LevelData data = JsonUtility.FromJson<LevelData>(json);

        tilemap.ClearAllTiles();
        
        for (int i = 0; i < data.tiles.Count; i++)
        {
            tilemap.SetTile(new Vector3Int(data.posX[i], data.posY[i], 0), tiles.Find(t => t.name == data.tiles[i]).tile);
        }
    }
}

public class LevelData
{
    public List<string> tiles = new List<string>();
    public List<int> posX = new List<int>();
    public List<int> posY = new List<int>();
}
