using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PleacableObject
{
    public Item placedItem;
    public Transform targetObject;
    public Vector3Int positionOnGrid;

    public PleacableObject(Item item, Transform target, Vector3Int pos)
    {
        placedItem = item;
        targetObject = target;
        positionOnGrid = pos;
    }
}

[CreateAssetMenu(menuName ="Data/Pleaceble Object Container")]
public class PleacableObjectContainer : ScriptableObject
{
    public List<PleacableObject> pleacableObjects;
}
