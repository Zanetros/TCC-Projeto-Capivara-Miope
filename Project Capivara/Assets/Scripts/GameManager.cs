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
    public RecipeDiscovery recipeDiscovery;
    public QuestController questController;
    public FishingController fishingController;
    public PlayerShopController playerShopController;
    public PlayerShopHandler playerShop;
    public NpcSpawnManager npcSpawnManager;
    public PlayerStamina playerStamina;
    public ToolBarController toolBarController;

    public void ControlCharacterControls(bool characterMov, bool characterInt)
    {
        playerMovement.enabled = characterMov;
        characterInteractController.enabled = characterInt;
    }

    public void ControlPassageOfTime(bool countingTime)
    {
        timeManager.ControlTime(countingTime);
    }

}
