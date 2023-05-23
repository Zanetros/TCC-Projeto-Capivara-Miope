using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleacableObjectsReferenceManager : MonoBehaviour
{
    public PleacableObjectManagaer pleacableObject;

    public void Place(Item item, Vector3Int pos)
    {
        if (pleacableObject == null)
        {
            Debug.LogWarning("sem referencia para pleacable manager");
            return;
        }

        pleacableObject.Place(item, pos);
    }
}
