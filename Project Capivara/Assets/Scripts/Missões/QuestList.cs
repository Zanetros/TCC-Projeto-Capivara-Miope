using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Quest/Quest List")]
public class QuestList : ScriptableObject
{
    public List<QuestContainer> quests;
    private QuestStage stageToFind;

    public void LoadOwnedQuest(QuestContainer ownedQuest, int stage)
    {
        ownedQuest.actualStage = stage;
        quests.Add(ownedQuest);
    }
    
    public void GetNewQuest(QuestContainer quest)
    {
        Debug.Log("Nova missão ativa!");
        quests.Add(quest);
    }

    public string GetStageObjective(QuestContainer quest)
    {
        foreach (QuestContainer questContainer in quests)
        {
            if (questContainer.questId.Equals(quest.questId))
            {
                return questContainer.stages[questContainer.actualStage].ShowObjective(quest.questActor);
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
