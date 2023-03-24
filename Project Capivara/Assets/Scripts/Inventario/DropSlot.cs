using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Draggable d;
    private InventorySlotManager i;
    private GameObject dToChange;
    public InventorySlotManager.typeOfSlot myType;
    public void OnPointerEnter(PointerEventData eventData)
    {
    
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        d = eventData.pointerDrag.GetComponent<Draggable>();
        if(d != null)
        {
            i = eventData.pointerDrag.GetComponent<InventorySlotManager>();
            if (myType.Equals(InventorySlotManager.typeOfSlot.Inventory) &&
                i.myType.Equals(InventorySlotManager.typeOfSlot.FromQuickBar))
            {
                if (transform.childCount > 0)
                {
                    dToChange = transform.parent.gameObject;
                    dToChange.transform.SetParent(d.parentToReturnTo);
                }
            }
            else
            {
                i.myType = InventorySlotManager.typeOfSlot.FromQuickBar;
                if (transform.childCount > 0)
                {
                    dToChange = transform.parent.gameObject;
                    dToChange.transform.SetParent(d.parentToReturnTo);
                }
                d.parentToReturnTo = transform;
            }
        }
    }
}