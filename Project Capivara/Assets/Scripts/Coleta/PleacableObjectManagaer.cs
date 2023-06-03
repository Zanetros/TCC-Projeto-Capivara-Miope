using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PleacableObjectManagaer : MonoBehaviour
{
    [SerializeField] PleacableObjectContainer pleacableObjects;
    [SerializeField] Tilemap targetTilemap;

    private void Start()
    {
        GameManager.instance.GetComponent<PleacableObjectsReferenceManager>().pleacableObjectsManager = this;
        VisualizeMap();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < pleacableObjects.pleacableObjects.Count; i++)
        {
            pleacableObjects.pleacableObjects[i].targetObject = null;
        }
    }

    private void VisualizeMap()
    {
        for (int i = 0; i < pleacableObjects.pleacableObjects.Count; i++)
        {
            VisualizeItem(pleacableObjects.pleacableObjects[i]);
        }
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        PleacableObject placedObject = (PleacableObject)pleacableObjects.Get(gridPosition);

        if (placedObject == null)
        {
            return;
        }

        DropedItemSpawner.instance.SpawnItem
            (targetTilemap.CellToWorld(gridPosition), placedObject.placedItem, 1);

        Destroy(placedObject.targetObject.gameObject);

        pleacableObjects.Remove(placedObject);
    }

    private void VisualizeItem(PleacableObject pleacableObject)
    {
        GameObject go = Instantiate(pleacableObject.placedItem.itemPrefab);
        Vector3 position = 
            targetTilemap.CellToWorld(pleacableObject.positionOnGrid)
            + targetTilemap.cellSize / 2;

        position += Vector3.forward * 0.1f;
        go.transform.position = position;

        pleacableObject.targetObject = go.transform;
    }

    public bool Check(Vector3Int position)
    {
        return pleacableObjects.Get(position) != null;
    }
    
    public void Place(Item item, Vector3Int positionOnGrid)
    {
        if (Check(positionOnGrid) == true)
        {
            return;
        }

        PleacableObject pleacableObject = new PleacableObject(item, positionOnGrid);
        VisualizeItem(pleacableObject);
        pleacableObjects.pleacableObjects.Add(pleacableObject);
    }
}
