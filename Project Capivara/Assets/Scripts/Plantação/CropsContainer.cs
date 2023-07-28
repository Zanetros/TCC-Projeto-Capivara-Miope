using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/ Crops Container")]
public class CropsContainer : ScriptableObject, ISerializationCallbackReceiver
{
    public List<CropsTile> crops;
    public string savePath;
    public CropDataBaseObject cropsDatabase;

    private void OnEnable()
    {
#if UNITY_EDITOR
        cropsDatabase = (CropDataBaseObject)AssetDatabase.LoadAssetAtPath
            ("Assets/Resources/CropDatabase.asset", typeof(CropDataBaseObject));
#else
        cropsDatabase = Resources.Load<CropDataBaseObject>("CropDatabase");
#endif
    }

    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {

    }

    public CropsTile Get(Vector3Int position)
    {
        return crops.Find(x => x.position == position);
    }

    internal void Add(CropsTile crop)
    {
        crops.Add(crop);
    }

    public void SaveCrops()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        binaryFormatter.Serialize(file, saveData);
        file.Close();
        Debug.Log("Plantas salvas");
    }

    public void LoadCrops()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(binaryFormatter.Deserialize(file).ToString(), this);
            file.Close();
        }
    }
}
