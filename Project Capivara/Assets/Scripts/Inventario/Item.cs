using UnityEngine;

[System.Serializable][CreateAssetMenu]
public class Item : ScriptableObject
{
    public string _name;
    public float value;
    public int quantity;
    public Sprite baseBackground;
    public Sprite itemSprite;
}
