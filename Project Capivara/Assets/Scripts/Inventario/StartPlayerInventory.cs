using System.Net;

[System.Serializable]
public class StartPlayerInventory
{
    private string[] startItems;
    private int[] startItemsQuantity;
    private float[] startHairColor = {70, 0, 0};
    
    public string[] GetAllItems()
    {
        return startItems;
    }

    public int[] GetAllItemsQuantity()
    {
        return startItemsQuantity;
    }

    public float[] GetHairColor()
    {
        return startHairColor;
    }
    
}
