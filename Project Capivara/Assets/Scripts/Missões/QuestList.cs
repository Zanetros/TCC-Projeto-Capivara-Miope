using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Quest/Quest List")]
public class QuestList : ScriptableObject
{
    public List<QuestContainer> quests;
    private QuestStage stageToFind;

    public void GetNewQuest(QuestContainer quest)
    {
        foreach (QuestContainer questContainer in quests)
        {
            if (questContainer.questId.Equals(quest.questId))
            {
                Debug.Log("Esta missão já está ativa");
                return;
            }    
        }
        Debug.Log("Nova missão ativa!");
        quests.Add(quest);
    }

    public string GetStageObjective(QuestContainer quest)
    {
        foreach (QuestContainer questContainer in quests)
        {
            if (questContainer.questId.Equals(quest.questId))
            {
                return questContainer.stages[questContainer.actualStage].ShowObjective();
            }
        }
        return null;
    }
    
    public bool AdvanceStage(QuestContainer quest)
    {
        foreach (QuestContainer questContainer in quests)
        {
            if (questContainer.questId.Equals(quest.questId))
            {
                questContainer.actualStage++;
                if (questContainer.actualStage >= questContainer.stages.Count)
                {
                    questContainer.compleated = true;
                    questContainer.active = false;
                    return true;
                }
            }
        }
        return false;
    }

    public void CompleateQuest(QuestContainer quest)
    {
        foreach (QuestContainer questContainer in quests)
        {
            if (questContainer.questId.Equals(quest.questId))
            {
                quests.Remove(questContainer);
            }
        }
    }
    
    
    

}
