using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class CropsTile
{
    public int growTimer;
    public int growthStage;
    public Crop crop;
    public SpriteRenderer renderer;
    public Vector3 position;

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

public class CropsManager : MonoBehaviour
{
    public TilemapCropsManager tileCropsManager;
    
    public void PickUp(Vector3Int position  )
    {
        if (tileCropsManager == null)
        {
            Debug.Log("sem crops manager");
            return;
        }

        tileCropsManager.PickUp(position);
    }

    public bool Check(Vector3Int position)
    {
        if (tileCropsManager == null)
        {
            Debug.Log("sem crops manager");
            return false;
        }

        return tileCropsManager.Check(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        if (tileCropsManager == null)
        {
            Debug.Log("sem crops manager");
            return;
        }

        tileCropsManager.Seed(position, toSeed);
    }

    public void Plow(Vector3Int position)
    {
        if (tileCropsManager == null)
        {
            Debug.Log("sem crops manager");
            return;
        }
        
        tileCropsManager.Plow(position);
    }
}
