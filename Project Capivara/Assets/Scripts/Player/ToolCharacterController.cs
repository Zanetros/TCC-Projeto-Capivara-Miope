using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolCharacterController : MonoBehaviour
{
    PlayerMovement playerMovement;
    Rigidbody2D rigidbody2D;
    Animator animator;
    ToolBarController toolbarController;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;

    #region Plantação
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] float maxDistance = 1.5f;
    [SerializeField] CropsManager cropsManager;
    [SerializeField] TileData plowableTile;
    #endregion

    Vector3Int selectedTilePosition;
    bool selectable;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        toolbarController = GetComponent<ToolBarController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SelectTile();
        CanSelectCheck();
        Marker();

        if (Input.GetMouseButtonDown(0))
        {
            if (UseToolWorld() == true)
            {
                return;
            }

            UseToolGrid();
        }
    }

    private void SelectTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }
    
    private void CanSelectCheck()
    {
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
        markerManager.Show(selectable);
    }

    private void Marker()
    {       
        markerManager.markedCellPosition = selectedTilePosition;
    }

    public bool UseToolWorld()
    {
        Vector2 position = rigidbody2D.position + playerMovement.lastMotionVector * offsetDistance;

        Item item = toolbarController.GetItem;
        if (item == null) { return false; }
        if (item.onAction == null) { return false; }

        animator.SetTrigger("act");
        bool complete = item.onAction.OnAply(position);

        return complete;
    }

    public void UseToolGrid()
    {
        if (selectable == true)
        {
            TileBase tileBase = tileMapReadController.GetTileBase(selectedTilePosition);
            TileData tileData = tileMapReadController.GetTileData(tileBase);
            if (tileData != plowableTile) { return; }
            
            if (cropsManager.Check(selectedTilePosition))
            {
                cropsManager.Seed(selectedTilePosition);
            }
            else
            {
                cropsManager.Plow(selectedTilePosition);
            }            
        }
    }
}
