using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TalkInteract : Interactable
{
    [SerializeField] private DialogueContainer dialogue;
    [SerializeField] private NpcWalkController npcWalkController;
    
    public override void Interact(Character character)
    {
        GameManager.instance.dialogueSystem.Initialize(dialogue);
        if (!npcWalkController.Equals(null))
        {
            GameManager.instance.dialogueSystem.npcTalking = npcWalkController;
            npcWalkController.CallTalking();
        }
    }
}
