using UnityEngine;

[System.Serializable][CreateAssetMenu]
public class Item : ScriptableObject
{
    public string name;
    public float value;
    public int quantity;
    public Sprite baseBackground;
    public Sprite itemSprite;
}
