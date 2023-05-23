using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public string Description;
    public int sellPrice;
    public bool stackable;
    public Sprite sprite;
    public ToolActions onAction;
    public ToolActions onTilemapAction;
    public ToolActions onItemUsed;
    public Crop crop;
    public bool iconHighlight;
    public GameObject itemPrefab;
    
    public Sprite icon { get; internal set; }
}
