using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
    
    public GameObject player;
    public ItemContainer inventoryContainer;
    public ItemDragAndDropController dragAndDropController;
    public DialogueSystem dialogueSystem;
    public TimeManager timeManager;
    public CoinBag coinBag;
    public ShopController shopController;
    public PlayerMovementTeste playerMovement;
    public CharacterInteractController characterInteractController;
    public CropsManager cropsManager;
    public PleacableObjectsReferenceManager pleacableObjects;
    public MusicManager musicManager;
    public InventoryController inventoryController;
    public Crafting crafting;

    public void ControlCharacterControls(bool characterMov, bool characterInt)
    {
        playerMovement.enabled = characterMov;
        characterInteractController.enabled = characterInt;
    }
    
}
