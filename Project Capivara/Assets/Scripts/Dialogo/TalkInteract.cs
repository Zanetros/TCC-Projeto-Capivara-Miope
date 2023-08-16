using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TalkInteract : Interactable
{
    [SerializeField] private NpcWalkController npcWalkController;
    
    public override void Interact(Character character)
    {
        GameManager.instance.dialogueSystem.Initialize(npcWalkController.myPersonality.dialogueContainer, null,false, false);
        GameManager.instance.ControlCharacterControls(false, false);
        GameManager.instance.ControlPassageOfTime(true);
        if (!npcWalkController.Equals(null))
        {
            GameManager.instance.dialogueSystem.npcTalking = npcWalkController;
            npcWalkController.CallTalking();
        }
    }
}
