using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDragAndDropController : MonoBehaviour
{
    [SerializeField] ItemSlot itemSlot;
    [SerializeField] GameObject dragItemIcon;
    RectTransform iconTransform;
    Image itemIconImage;

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
        }
        else
        {
            Item item = itemSlot.item;
            int count = itemSlot.count;

            itemSlot.Copy(this.itemSlot);
            this.itemSlot.Set(item, count);
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
}
