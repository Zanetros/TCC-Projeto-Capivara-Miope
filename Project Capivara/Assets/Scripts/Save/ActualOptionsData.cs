using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActualOptionsData
{
    public int actualDay;
    public int[,] playerItens;
    public int[,] itensInChest;
    public float[,] itensOnMap;
    public int money;
    public int[,] questsActive;
    public int[] knownRecipes;
    public float musicV;
    public float sfxV;
    //Em cada linha do Array, são salvas as seguintes 5 informações:
    //{0,0,0,0,0}
    //{Index Da Cena (Na Aba de Build) onde a Crop está, Id Da Crop, Tempo de Crescimento Atual, Posição em X no Mapa, Posição em Y no Mapa}
    public float[,] cropsOnMap;
    public int[,] npcRelationship;

    public int resetDay()
    {
        return 1;
    }

    public int[,] ResetPlayerItens()
    {
        return new int[,]
        {{0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0},
            {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}};
    }
    
    public int[,] ResetItensOnChest()
    {
        return new int[,]
        {{0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0},
            {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}, {0,0}};
    }
    
    public float[,] ResetItensOnMap()
    {
        return new float[,]
        {
            {0,0,0,0}, {0,0,0,0}
        };
    }

    public int ResetMoney()
    {
        return 100;
    }

    public int[,] ResetQuests()
    {
        return new int[,] { {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}};
    }

    public int[] ResetKnownRecipes()
    {
        return new int[]{-1};
    }
    
    public float ResetMusic()
    {
        return 0F;
    }

    public float ResetSfx()
    {
        return 0F;
    }
    
    public float[,] ResetCropsOnMap()
    {
        //Marcar aqui posteriormente os limoeiros e a posição deles na fazenda do jogador
        return new float[,] {{0,0,0,0,0}, {0,0,0,0,0}, {0,0,0,0,0}};
    }

    public int[,] ResetNpcsRelationship()
    {
        return new int[,] { };
    }
    
}