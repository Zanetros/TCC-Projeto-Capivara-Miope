using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotManager : MonoBehaviour
{
    private Item myItem;
    public Image background;
    public Image itemSprite;
    public TextMeshProUGUI txtQuantity;
    
    public void Populate(Item newItem)
    {
        gameObject.SetActive(true);
        myItem = newItem;
        background.sprite = myItem.baseBackground;
        itemSprite.sprite = myItem.itemSprite;
        txtQuantity.text = myItem.quantity.ToString();
    }
    
}
