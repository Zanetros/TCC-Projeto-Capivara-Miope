
using UnityEngine;

[System.Serializable]
public class PlayerInventory
{
    private string[] _item;
    private int[] itemQuantity;
    private float[] hairColor = {70, 0, 0};
    
    public string getItemName(int index)
    {
        return _item[index];
    }

    public void setItemName(int index, string item)
    {
        _item[index] = item;
    }
    
    public int getItemQuantity(int index)
    {
        return itemQuantity[index];
    }

    public void setItemQuantity(int index, int quantity)
    {
        itemQuantity[index] = quantity;
    }

    public float[] getHairColor()
    {
        return hairColor;
    }

    public void setHairColor(float[] newColor)
    {
        for (int i = 0, j = hairColor.Length; i < j; i++)
        {
            hairColor[i] = newColor[i];
        }
    }
    
    public void SetAllItems(string[] items)
    {
        _item = items;
    }

    public void SetAllItemsQuantity(int[] quantity)
    {
        itemQuantity = quantity;
    }
    
    public string[] GetAllItems()
    {
        return _item;
    }

    public int[] GetAllItemsQuantity()
    {
        return itemQuantity;
    }
    
}
