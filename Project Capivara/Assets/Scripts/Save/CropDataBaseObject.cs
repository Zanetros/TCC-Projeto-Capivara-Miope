using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "CropDatabase")]
public class CropDataBaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public Crop[] Crops;
    public Dictionary<Crop, int> GetId = new Dictionary<Crop, int>();
    public Dictionary<int, Crop> GetCrop = new Dictionary<int, Crop>();

    public void OnAfterDeserialize()
    {
        GetId = new Dictionary<Crop, int>();
        GetCrop = new Dictionary<int, Crop>();
        for (int i = 0; i < Crops.Length; i++)
        {
            GetId.Add(Crops[i], i);
            GetCrop.Add(i, Crops[i]);
        }
    }

    public void OnBeforeSerialize()
    {

    }
}
