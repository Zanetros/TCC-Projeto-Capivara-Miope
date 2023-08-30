using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class TilemapCropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;

    Tilemap targetTilemap;

    [SerializeField] GameObject cropsSpritePrefab;

    public CropsContainer container;
    public List<SpriteRenderer> cropsRenders;
    public List<int> c;
    private int d = 0;
    public TimeManager timeManager;
    
    public void Start()
    {
        d = 0;
        GameManager.instance.GetComponent<CropsManager>().tileCropsManager = this;
        targetTilemap = GetComponent<Tilemap>();
        timeManager = FindObjectOfType<TimeManager>();
        Init();
        onTimeTick += Tick;
        VisualizeMap();
    }

    public void Update()
    {
        
    }

    private void OnEnable()
    {
        TimeManager.OnDayChanged += GrowStage;
    }

    private void OnDisable()
    {
        TimeManager.OnDayChanged -= GrowStage;
    }

    public void GrowStage()
    {
        foreach (CropsTile crops in container.crops)
        {
            if (crops.crop == null) { continue; }

            if (crops.Complete)
            {
                continue;
            }
  
            crops.growthStage += 1;
            targetTilemap.SetTile(crops.position, plowed);

            if (crops.growthStage == crops.crop.maxGrowthFase -1)
            {
                crops.isGrown = true;
                crops.renderer.sprite = crops.crop.sprites[crops.growthStage];
            }

            else
            {               
                crops.renderer.sprite = crops.crop.sprites[crops.growthStage];
            }
            
        }
        
    }

    private void VisualizeMap()
    {
        for (int i = 0; i < container.crops.Count; i++)
        {
            VisualizeTile(container.crops[i]);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < container.crops.Count; i++)
        {
            container.crops[i].renderer = null;
        }
    }

    public void Tick()
    {
        foreach (CropsTile cropsTile in container.crops)
        {
            if (cropsTile.crop == null) { continue; }

            if (cropsTile.Complete)
            {
                continue;
            } 
            
            cropsTile.renderer.gameObject.SetActive(true);
            cropsTile.crop.maxGrowthFase = cropsTile.crop.sprites.Count;
        }

        for (int i = 0; i < cropsRenders.Count; i++)
        {
            if (container.crops[i].crop != null && container.crops[i].growthStage >= container.crops[i].crop.maxGrowthFase)
            {
                cropsRenders[i].gameObject.SetActive(true);
                cropsRenders[i].sprite = container.crops[i].crop.sprites[container.crops[i].growthStage];
            }
        }
    }

    internal bool Check(Vector3Int position)
    {
        return container.Get(position) != null;
    }

    public void Plow(Vector3Int position)
    {
        if (Check(position) == true) { return; }
        CreatePlowedTile(position);
        
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        CropsTile tile = container.Get(position);

        if (tile == null) { return; }
        
        targetTilemap.SetTile(position, seeded);

        tile.crop = toSeed;
        tile.sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        tile.renderer.gameObject.SetActive(true);
        for (int i = 0; i < tile.crop.sprites.Count; i++)
        {
            tile.renderer.sprite = tile.crop.sprites[0];
        }
    }

    public void VisualizeTile(CropsTile cropsTile)
    {
        targetTilemap.SetTile(cropsTile.position, cropsTile.crop != null ? seeded : plowed);           
        
        if (cropsTile.renderer == null) 
        {
            GameObject go = Instantiate(cropsSpritePrefab, transform);
            go.transform.position = targetTilemap.CellToWorld(cropsTile.position);
            go.transform.position -= Vector3.forward * 0.01f;
            cropsRenders.Add(go.GetComponent<SpriteRenderer>());
            cropsTile.renderer = go.GetComponent<SpriteRenderer>();
            c.Add(d);
        }

        bool growing = cropsTile.crop != null && cropsTile.growthStage >= cropsTile.crop.maxGrowthFase;

        cropsTile.renderer.gameObject.SetActive(growing);

        if (growing == true)
        {
            cropsTile.renderer.sprite = cropsTile.crop.sprites[cropsTile.growthStage - 1];
            cropsRenders[d].sprite = cropsTile.crop.sprites[cropsTile.growthStage - 1];
        }
        d++;
    }
    
    private void CreatePlowedTile(Vector3Int position)
    {   
            print("arado");
            CropsTile crop = new CropsTile();
            container.Add(crop);
                    
            crop.position = position;

            VisualizeTile(crop);

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
            tile.isGrown = false;
            tile.Harvested();
            VisualizeTile(tile);
        }
    }
}
