using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotManager : MonoBehaviour
{
    public Item myItem;
    public Image background;
    public Image itemSprite;
    public TextMeshProUGUI txtQuantity;

    public enum typeOfSlot { FromQuickBar, Inventory};
    public typeOfSlot myType = typeOfSlot.Inventory;
    
    public void Populate(Item newItem)
    {
        if (myType.Equals(typeOfSlot.Inventory))
        {
            gameObject.SetActive(true);
        }
        myItem = newItem;
        background.enabled = true;
        itemSprite.enabled = true;
        background.sprite = myItem.baseBackground;
        itemSprite.sprite = myItem.itemSprite;
        txtQuantity.text = myItem.quantity.ToString();
    }

    public void Deadctivate()
    {
        if (myType.Equals(typeOfSlot.Inventory))
        {
            gameObject.SetActive(false);
        }
        background.enabled = false;
        itemSprite.enabled = false;
        background.sprite = null;
        itemSprite.sprite = null;
        txtQuantity.text = "";
    }
    
}
