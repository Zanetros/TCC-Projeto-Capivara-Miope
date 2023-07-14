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
    public Vector3Int position;

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

    public void LoadCropsPlowed(int[,] crops)
    {
        //0 - Id da Crop
        //1 - Estágio de Crescimento
        //2 - Id da Cena
        //3 - Posição em X
        //4 - Posição em Y
        for (int i = 0; i < crops.GetLength(0); i++) 
        {
            for (int j = 0; j < crops.GetLength(1); j++) 
            { 
                
            }
        }  
    }
}
