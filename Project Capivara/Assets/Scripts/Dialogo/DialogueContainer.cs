using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Dialogue/Dialogues for Character")]
public class DialogueContainer :ScriptableObject
{
    public List<DialogueByDay> lineInEachDay;
    public Actor actor;
}
