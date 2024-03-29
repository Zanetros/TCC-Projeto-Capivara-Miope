using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

[Serializable]
public class CropsTile
{
    public int growthStage;
    public int sceneBuildIndex;
    public Crop crop;
    public bool isGrown;
    public SpriteRenderer renderer;
    public Vector3Int position;

    public bool Complete
    {
        get
        {
            if (crop == null) { return false; }
            return isGrown;
}
    }

    internal void Harvested()
    {
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
