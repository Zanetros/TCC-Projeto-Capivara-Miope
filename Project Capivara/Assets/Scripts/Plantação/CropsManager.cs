using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

[Serializable]
public class CropsTile
{
    public int growTimer;
    public int growthStage;
    public int sceneBuildIndex;
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
    private float[,] cropsToReturn = {{0,0,0,0,0,0}, {0,0,0,0,0,0}, {0,0,0,0,0,0}};
    private int c = 0;
    
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

    public void LoadCropsPlowed(float[,] crops)
    {
        //0 - Id da Cena
        //1 - Id da Crop
        //2 - Tempo de Crescimento Atual
        //3 - Estágio de Crescimento Atual
        //4 - Posição em X
        //5 - Posição em Y
        for (int i = 0; i < crops.GetLength(0); i++) 
        {
            //Caso a crop esteja presente na scene atual carregada
            if (crops[i, 0].Equals(SceneManager.GetActiveScene().buildIndex))
            {
                //Colocar a Crop no Mapa. Vai ser necessário colocar uma lista de todas as crops do jogo no script 
                //TilemapCropsManager e compara o id das crops de lá com o Id informado aqui, que será:
                
                //crops[i, 1] -> Id da Crop*

                
                //*Fiz uma função chamada "TestarComparacaoDeCrop" no início do TilemapCropsManager pra vc ver como a comparação
                //pode ser feita
                
                
                
                //A posição dela no mapa será:
                
                //crops[i, 4] -> Posição em X da Crop
                //crops[i, 5] -> Posição em Y da Crop
                
                
                
                //Setar o tempo de crescimento e o estágioo de crescimento da Crop colocada no mapa. Eles serão:
                
                //crops[i, 2] -> Tempo de Crescimento atual da Crop
                //crops[i, 3] -> Estágio de Crescimento atual da Crop
                
            }
        }  
    }

    public float[,] GetCropsPlowed()
    {
        c = 0;
        //To-Do: Ajustar essa função para que funcione corretamente (e qu)
        foreach (CropsTile crops in tileCropsManager.container.crops)
        {
            cropsToReturn[c, 0] = crops.sceneBuildIndex;
            cropsToReturn[c, 1] = crops.crop.cropId;
            cropsToReturn[c, 2] = crops.growTimer;
            cropsToReturn[c, 3] = crops.growthStage;
            cropsToReturn[c, 4] = crops.position.x;
            cropsToReturn[c, 5] = crops.position.y;
            c++;
        }
        
        return cropsToReturn;
    }
    
}
