using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/NPC/Npc Day Task")]
public class NpcTimeAndLocation : ScriptableObject
{
    public int hour;
    public int minute;
    public int sceneToBeIn;
}
