using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Quest/Quest Stage")]
public class QuestStage : ScriptableObject
{
        public ItemSlot[] itensToReceive;
        private string objectiveExplained = null;

        public string ShowObjective(Actor actor)
        {
                objectiveExplained = null;
                foreach (ItemSlot itemSlot in itensToReceive)
                {
                        if (objectiveExplained == null)
                        {
                                objectiveExplained = actor.name + " pede " + itemSlot.count + " "
                                                     + itemSlot.item.Name + "(s)";
                        }
                        else
                        {
                                objectiveExplained += ", " + itemSlot.count + " "
                                                      + itemSlot.item.Name + "(s)";
                        }
                }
                return objectiveExplained;
        }

}