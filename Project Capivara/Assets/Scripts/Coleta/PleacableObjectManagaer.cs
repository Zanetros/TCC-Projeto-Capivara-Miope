using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PleacableObjectManagaer : MonoBehaviour
{
    [SerializeField] PleacableObjectContainer pleacableObject;
    [SerializeField] Tilemap targetTilemap;

    private void Start()
    {
        GameManager.instance.GetComponent<PleacableObjectsReferenceManager>().pleacableObject = this;
    }

    public void Place(Item item, Vector3Int positionOnGrid)
    {
        GameObject go = Instantiate(item.itemPrefab);
        Vector3 position = targetTilemap.CellToWorld(positionOnGrid) + targetTilemap.cellSize/2;
        position += Vector3.forward * 0.1f;
        go.transform.position = position;
        pleacableObject.pleacableObjects.Add(new PleacableObject(item, go.transform, positionOnGrid));
    }
}
