using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Save_And_Load_Options : MonoBehaviour
{

    [Header("Player")]
    #region
    public int actualDay;
    public GameManager gameManager;
    public int[,] npcRelationship;
    #endregion

    [Header("Quests")]
    #region
    public QuestList questsActive;
    #endregion
    
    #region
    [Header("Options")]
    //public SoundController sC;
    public float music;
    public float sFX;
    #endregion

    private void Start()
    {
        if (CallLoad())
        {
            //sC.LoadSoundOptions(music, sFX);   
            
        }
        else
        {
            ResetOptions();
            CallLoad();
        }
    }
    
    public void UpdateMusicToggle(float musicR)
    {
        music = musicR;
    }

    public void UpdateSFXVolume(float sfxR)
    {
        sFX = sfxR;
    }

    public void UpdateSave()
    {
        //CallSaveOptions(actualDay, gameManager.inventoryContainer.GetItensInInventory(), music, sFX, false);
        CallLoad();
    }

    public bool CallLoad()
    { 
        ActualOptionsData aOD = LoadOptions();
        if (aOD != null)
        {
            actualDay = aOD.actualDay;
            gameManager.inventoryContainer.LoadItensToInventory(aOD.playerItens);
            //Setar os Itens para o Baú na casa do Jogador
            //Idem anterior, mas para os itens no mapa
            gameManager.coinBag.SetCoints(aOD.money);
            questsActive = gameManager.questController.LoadQuests(aOD.questsActive);
            gameManager.crafting.LoadKnownRecipes(aOD.knownRecipes);
            music = aOD.musicV;
            sFX = aOD.sfxV;
            gameManager.cropsManager.LoadCropsPlowed(aOD.cropsOnMap);
            ////Idem os 2 faltantes, mas para os relacionamentos com os Npcs
            npcRelationship = aOD.npcRelationship;
            
            return true;
        }
        return false;
    }

    public void CallSaveOptions(int actualDay, int[,] playerItens, int[,] itensInChest, float[,] itensOnMap,
        int money, int[,] questsActive, int[] knownRecipes, float musicR, float sFXR, float[,] cropsOnMap,
        int[,] npcRelationship, bool isNew)
    {
        ActualOptionsData aOD = new ActualOptionsData();
       
        aOD.musicV = musicR;
        aOD.sfxV = sFXR;

        if (!isNew)
        {
            SaveOptions(aOD, true);
            return;
        }
        SaveOptions(aOD, false);
    }

    void SaveOptions(ActualOptionsData aOD, bool isNew)
    {
        BinaryFormatter bF = new BinaryFormatter();

        string path = Application.persistentDataPath;

        if (!isNew)
        {
            File.Delete(path + "/playerOptions.save");
        }

        FileStream file = File.Create(path + "/playerOptions.save");
        bF.Serialize(file, aOD);
        file.Close();
    }

    public ActualOptionsData LoadOptions()
    {
        BinaryFormatter bF = new BinaryFormatter();
        FileStream file;
        string path = Application.persistentDataPath;

        if (File.Exists(path + "/playerOptions.save"))
        {
            file = File.Open(path + "/playerOptions.save", FileMode.Open);
            ActualOptionsData aOD = (ActualOptionsData)bF.Deserialize(file);
            file.Close();
            return aOD;
        }
        return null;
    }

    public void ResetOptions()
    {
        ActualOptionsData aoD = new ActualOptionsData();
        CallSaveOptions(aoD.resetDay(), aoD.ResetPlayerItens(), aoD.ResetItensOnChest(), aoD.ResetItensOnMap(), aoD.ResetMoney(),
            aoD.ResetQuests(), aoD.ResetKnownRecipes(), aoD.ResetMusic(), aoD.ResetSfx(), aoD.ResetCropsOnMap(), aoD.ResetNpcsRelationship(), true);
    }

}