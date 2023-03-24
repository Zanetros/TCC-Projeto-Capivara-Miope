using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturnTo = null;
    private Transform oldD = null;
    private int newSiblingIndex;
    public GameObject placeholder;
    public InventoryManager iM;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (placeholder != null)
        {
            placeholder.SetActive(true);
            placeholder.transform.SetParent(transform.parent);
            LayoutElement lE = placeholder.AddComponent<LayoutElement>();
            lE.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
            lE.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
            lE.flexibleWidth = GetComponent<LayoutElement>().flexibleWidth;
            lE.flexibleHeight = GetComponent<LayoutElement>().flexibleHeight;
            placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());   
        }
        oldD = transform.parent;
        parentToReturnTo = transform.parent;
        transform.SetParent(transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        newSiblingIndex = parentToReturnTo.childCount;
        
        for (int i = 0; i < parentToReturnTo.childCount; i++)
        {
            if (transform.position.x < parentToReturnTo.GetChild(i).position.x)
            {
                newSiblingIndex = i;
                if (placeholder != null && placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                }
                break;
            }
        }

        if (placeholder != null)
        {
            placeholder.transform.SetSiblingIndex(newSiblingIndex);
            placeholder.GetComponent<InventorySlotManager>().
                Populate(eventData.pointerDrag.GetComponent<InventorySlotManager>().myItem);   
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentToReturnTo);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        GetComponent<InventorySlotManager>().myType = parentToReturnTo.gameObject.GetComponent<DropSlot>().myType;
        //iM.UpdateItemLocation(GetComponent<InventorySlotManager>());
        
        if (placeholder != null)
        {
            iM.PopulateInventory();
            transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
            placeholder.SetActive(false);
            placeholder.transform.SetParent(null);
            placeholder.GetComponent<InventorySlotManager>().Deadctivate();
        }
    }

}
