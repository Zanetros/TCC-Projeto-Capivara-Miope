using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemDragAndDropController : MonoBehaviour
{
    public ItemSlot itemSlot;
    public GameObject dragItemIcon;
    RectTransform iconTransform;
    Image itemIconImage;
    public bool isDraging;
    public bool inPlace;

    [SerializeField] PlayerShopHandler playerShop;
    public Image iconToSellImage;
    public TextMeshProUGUI textItemToSell;

    private void Start()
    {
        itemSlot = new ItemSlot();
        iconTransform = dragItemIcon.GetComponent<RectTransform>();
        itemIconImage = dragItemIcon.GetComponent<Image>();
    }

    private void Update()
    {
        if (dragItemIcon.activeInHierarchy == true)
        {
            iconTransform.position = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    worldPosition.z = 0;

                    DropedItemSpawner.instance.SpawnItem(
                        worldPosition, 
                        itemSlot.item, 
                        itemSlot.count);

                    itemSlot.Clear();
                    dragItemIcon.SetActive(false);
                }
            }
        }
    }

    internal void OnClick(ItemSlot itemSlot)
    {
        if (this.itemSlot.item == null)
        {
            this.itemSlot.Copy(itemSlot); 
            itemSlot.Clear();
            isDraging = true;
        }
        else
        {
            Item item = itemSlot.item;
            int count = itemSlot.count;
            int id = itemSlot.id;

            itemSlot.Copy(this.itemSlot);
            this.itemSlot.Set(item, count, id);
            isDraging = false ;
        }
        UpdateIcon();      
    }

    private void UpdateIcon()
    {
        if (itemSlot.item == null)
        {
            dragItemIcon.SetActive(false);
        }

        else
        {
            dragItemIcon.SetActive(true);
            itemIconImage.sprite = itemSlot.item.sprite;
        }
    }

    public void TransferSlot()
    {
        if (inPlace)
        {
            itemSlot.Copy(playerShop.itemSlot);
            itemIconImage.sprite = playerShop.itemSlot.item.sprite;
            iconToSellImage.sprite = null;
            textItemToSell.text = null;
            dragItemIcon.SetActive(true);
            playerShop.itemSlot.Clear();
            inPlace = false;
        }

        else
        {
            playerShop.itemSlot.Copy(itemSlot);
            iconToSellImage.sprite = itemSlot.item.sprite;
            textItemToSell.text = itemSlot.count.ToString();
            dragItemIcon.SetActive(false);
            itemSlot.Clear();
            inPlace = true;
        }
              
    }

    public void AfterSell()
    {
        playerShop.itemSlot.Clear();
        iconToSellImage.sprite = null;
        textItemToSell.text = null;
        inPlace = false;
        GameManager.instance.playerShop.SetItensInShop();
    }
}
