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

    public PleacableObject(Item item, Vector3Int pos)
    {
        placedItem = item;
        positionOnGrid = pos;
    }
}

[CreateAssetMenu(menuName ="Data/Pleaceble Object Container")]
public class PleacableObjectContainer : ScriptableObject
{
    public List<PleacableObject> pleacableObjects;

    internal object Get(Vector3Int position)
    {
        return pleacableObjects.Find(x => x.positionOnGrid == position);
    }

    internal void Remove(PleacableObject placedObject)
    {
        pleacableObjects.Remove(placedObject);
    }
}
