using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleacableObjectsReferenceManager : MonoBehaviour
{
    public PleacableObjectManagaer pleacableObjectsManager;

    public void Place(Item item, Vector3Int pos)
    {
        if (pleacableObjectsManager == null)
        {
            Debug.LogWarning("sem referencia para pleacable manager");
            return;
        }

        pleacableObjectsManager.Place(item, pos);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        if (pleacableObjectsManager == null)
        {
            Debug.LogWarning("sem referencia para pleacable manager");
            return;
        }

        pleacableObjectsManager.PickUp(gridPosition);
    }

    public bool Check(Vector3Int pos)
    {
        if (pleacableObjectsManager == null)
        {
            Debug.Log("No pleaceble object");
            return false;
        }

        return pleacableObjectsManager.Check(pos);
    }
}
