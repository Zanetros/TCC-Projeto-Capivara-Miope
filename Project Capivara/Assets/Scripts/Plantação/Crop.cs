using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Crop")]
public class Crop : ScriptableObject
{
    public int cropId;
    public Item yield;
    public int count = 1;

    public int maxGrowthFase;
    public List<Sprite> sprites;
}
