using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapCropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;

    Tilemap targetTilemap;

    [SerializeField] GameObject cropsSpritePrefab;

    [SerializeField] CropsContainer container;

    public void Start()
    {
        GameManager.instance.GetComponent<CropsManager>().cropsManager = this;
        targetTilemap = GetComponent<Tilemap>();
        onTimeTick += Tick;
        Init();
    }

    public void Tick()
    {
        foreach (CropsTile cropsTile in container.crops)
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

    internal bool Check(Vector3Int position)
    {
        return container.Get(position) != null;
    }

    public void Plow(Vector3Int position)
    {
        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        CropsTile tile = container.Get(position);

        if (tile == null) { return; }
        
        targetTilemap.SetTile(position, seeded);

        tile.crop = toSeed;
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        print("arado");
        CropsTile crop = new CropsTile();
        container.Add(crop);

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
        CropsTile tile = container.Get(gridPosition); 

        if (tile.Complete)
        {
            print("plantavelpego");
            DropedItemSpawner.instance.SpawnItem(targetTilemap.CellToWorld(gridPosition),
                tile.crop.yield, tile.crop.count);

            targetTilemap.SetTile(gridPosition, plowed);
            tile.Harvested();
        }
    }
}
