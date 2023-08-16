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
    public CropsContainer allCropsInGame;
    public List<SpriteRenderer> cropsRenders;
    public List<int> c;
    private int d = 0;

    //Função de Exemplo
    public void TestarComparacaoDeCrop(int id)
    {
        foreach (CropsTile cropToCompare in allCropsInGame.crops)
        {
            if (cropToCompare.crop.cropId.Equals(id))
            {
                Debug.Log("O id informado pertence a uma crop do jogo");
            }
            else
            {
                Debug.Log("O id informado não pertence a uma crop do jogo");
            }
        }
    }
    
    public void Start()
    {
        d = 0;
        GameManager.instance.GetComponent<CropsManager>().tileCropsManager = this;
        targetTilemap = GetComponent<Tilemap>();
        onTimeTick += Tick;
        Init();
        VisualizeMap();
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

        for (int i = 0; i < cropsRenders.Count; i++)
        {
            if (container.crops[i].crop != null && 
                container.crops[i].growTimer >= container.crops[i].crop.growthStageTime[container.crops[i].growthStage])
            {
                cropsRenders[i].gameObject.SetActive(true);
                cropsRenders[i].sprite = container.crops[i].crop.sprites[container.crops[i].growthStage];
                if (container.crops[i].growthStage + 1 < container.crops[i].crop.sprites.Count)
                {
                    container.crops[i].growthStage += 1;
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
        bool growing = cropsTile.crop != null && cropsTile.growTimer >= cropsTile.crop.growthStageTime[0];
        
        cropsTile.renderer.gameObject.SetActive(growing);

        if (growing == true)
        {
            cropsTile.renderer.sprite = cropsTile.crop.sprites[cropsTile.growthStage-1];
            cropsRenders[d].sprite = cropsTile.crop.sprites[cropsTile.growthStage-1];
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

            tile.Harvested();
            VisualizeTile(tile);
        }
    }
}
