using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TalkInteract : Interactable
{
    [SerializeField] private DialogueContainer dialogue;
    
    public virtual void Interact(Character character)
    {
        GameManager.instance.dialogueSystem.Initialize(dialogue);
    }
}
