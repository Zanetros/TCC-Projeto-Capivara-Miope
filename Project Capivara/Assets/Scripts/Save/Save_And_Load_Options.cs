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
    public ItemContainer itensInGame;
    public ItemContainer itensInChest;
    public int money;
    public RecipeList knownRecipes;
    public int[,] npcRelationship;
    #endregion

    [Header("Quests")]
    #region
    public QuestList questsActive;
    #endregion
    
    #region
    [Header("Options")]
    public SoundController sC;
    public float music;
    public float sFX;
    public string[] x;
    #endregion

    private void Start()
    {
        if (CallLoad())
        {
            sC.LoadSoundOptions(music, sFX, x);   
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
        CallSaveOptions(music, sFX, x, false);
        CallLoad();
    }

    public bool CallLoad()
    { 
        ActualOptionsData aOD = LoadOptions();
        if (aOD != null)
        {
            music = aOD.musicV;
            sFX = aOD.sfxV;
            x = aOD.x;

            return true;
        }
        return false;
    }

    public void CallSaveOptions(float musicR, float sFXR, string[] x, bool isNew)
    {
        ActualOptionsData aOD = new ActualOptionsData();
       
        aOD.musicV = musicR;
        aOD.sfxV = sFXR;
        aOD.x = x;
        
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
        CallSaveOptions(aoD.ResetMusic(), aoD.ResetSfx(), aoD.ResetX(), true);
    }

}