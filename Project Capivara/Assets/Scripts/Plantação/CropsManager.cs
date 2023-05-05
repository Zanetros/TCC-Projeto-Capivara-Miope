using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropsTile
{
    public int growTimer;
    public int growthStage;
    public Crop crop;
    public SpriteRenderer renderer;

    public bool Complete
    {
        get
        {
            if (crop == null) { return false; }
            return growTimer >= crop.timeToGrow;
        }
    }

    internal void Harvested()
    {
        growTimer = 0;
        growthStage = 0;
        crop = null;
        renderer.gameObject.SetActive(false);
    }
}

public class CropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;

    [SerializeField] Tilemap targetTilemap;
    [SerializeField] GameObject cropsSpritePrefab;

    [SerializeField] Dictionary<Vector2Int, CropsTile> crops;

    private void Start()
    {
        crops = new Dictionary<Vector2Int, CropsTile>();
        onTimeTick += Tick;
        Init();
    }

    public void Tick()
    {
        foreach (CropsTile cropsTile in crops.Values)
        {
            if (cropsTile.crop == null) { continue; }

            if (cropsTile.Complete)
            {
                Debug.Log("cresceu");
                continue;
            }

            cropsTile.growTimer += 1;
            
            if (cropsTile.growTimer >= cropsTile.crop.growthStageTime[cropsTile.growthStage])
            {
                cropsTile.renderer.gameObject.SetActive(true);
                cropsTile.renderer.sprite = cropsTile.crop.sprites[cropsTile.growthStage];

                if (cropsTile.growthStage + 1 < cropsTile.crop.sprites.Count)
                {
                    cropsTile.growthStage += 1;   
                }
            }
        }
    }

    public bool Check(Vector3Int position)
    {
        return crops.ContainsKey((Vector2Int)position);
    }

    public void Plow(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))
        {
            return;
        }

        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        targetTilemap.SetTile(position, seeded);

        crops[(Vector2Int)position].crop = toSeed;
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        print("arado");
        CropsTile crop = new CropsTile();
        crops.Add((Vector2Int)position, crop);

        GameObject go = Instantiate(cropsSpritePrefab);
        go.transform.position = targetTilemap.CellToWorld(position);
        go.transform.position -= Vector3.forward * 0.01f;
        go.SetActive(false);
        crop.renderer = go.GetComponent<SpriteRenderer>();

        targetTilemap.SetTile(position, plowed);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        print("Pego");
        Vector2Int position = (Vector2Int)gridPosition;
        if (crops.ContainsKey(position) == false) { return; }

        CropsTile cropsTile = crops[position];

        if (cropsTile.Complete)
        {
            print("plantavelpego");
            DropedItemSpawner.instance.SpawnItem(targetTilemap.CellToWorld(gridPosition),
                cropsTile.crop.yield, cropsTile.crop.count);

            targetTilemap.SetTile(gridPosition, plowed);
            cropsTile.Harvested();
        }
    }
}
