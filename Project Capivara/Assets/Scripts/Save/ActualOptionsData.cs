using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActualOptionsData
{
    public int actualDay;
    public int[,] playerItens;
    public int[,] itensInChest;
    public int[,] itensOnMap;
    public int money;
    public int[,] questsActive;
    public int[] knownRecipes;
    public float musicV;
    public float sfxV;
    public int[,] cropsOnMap;
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
    
    public int[,] ResetItensOnMap()
    {
        return new int[,]
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
        return new int[,] { {0, 0}};
    }

    public int[] ResetKnownRecipes()
    {
        return new int[]{};
    }
    
    public float ResetMusic()
    {
        return 0F;
    }

    public float ResetSfx()
    {
        return 0F;
    }
    
    public int[,] ResetCropsOnMap()
    {
        //Marcar aqui posteriormente os limoeiros e a posição deles na fazenda do jogador
        return new int[,] {{0,0,0,0,0}, {0,0,0,0,0}, {0,0,0,0,0}};
    }

    public int[,] ResetNpcsRelationship()
    {
        return new int[,] { };
    }
    
}
