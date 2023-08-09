using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/NPC/Npc Container")]
public class NpcContainer : ScriptableObject
{
    public List<Npc> npcsFromScene;
    public int sceneBuildIndex;
}
