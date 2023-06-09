using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image highlight;
    int myIndex;

    public void SetIndex(int index)
    {
        myIndex = index;
    }

    public void Set(ItemSlot slot, int index)
    {
        SetIndex(index);
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.sprite;

        if (slot.item.stackable == true) 
        {
            text.gameObject.SetActive(true);
            text.text = slot.count.ToString();
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }

    public void UpdateItem(ItemSlot slot)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.sprite;

        if (slot.item.stackable == true) 
        {
            text.gameObject.SetActive(true);
            text.text = slot.count.ToString();
        }

        else
        {
            text.gameObject.SetActive(false);
        }
    }

    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);

        text.gameObject.SetActive(false);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        ItemPanel itemPanel = transform.parent.GetComponent<ItemPanel>();
        itemPanel.OnClick(myIndex);
    }

    public void Highlight (bool b)
    {
        highlight.gameObject.SetActive(b);
    }
}
