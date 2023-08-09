using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/NPC/Npc Data")]
public class Npc : ScriptableObject
{
    public DialogueContainer dialogueContainer;
    public Sprite npcSprite;
    public List<NpcTimeAndLocation> npcTimeAndLocations;
}
