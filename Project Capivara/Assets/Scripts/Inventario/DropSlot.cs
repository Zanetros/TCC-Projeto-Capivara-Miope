using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Draggable d;
    private GameObject dToChange;

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
            if(this.transform.childCount > 0)
            {
                dToChange = this.transform.parent.gameObject;
                dToChange.transform.SetParent(d.parentToReturnTo);
            }
            d.parentToReturnTo = this.transform;
        }
    }
}