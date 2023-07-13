using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Quest/Quest Stage")]
public class QuestStage : ScriptableObject
{
        public ItemSlot[] itensToReceive;
        private string objectiveExplained = null;

        public string ShowObjective()
        {
                return GetItensFromQuest();
        }

        private string GetItensFromQuest()
        {
                objectiveExplained = null;
                foreach (ItemSlot itemSlot in itensToReceive)
                {
                        if (objectiveExplained == null)
                        {
                                objectiveExplained = "Um NPC pede: " + itemSlot.count + " "
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