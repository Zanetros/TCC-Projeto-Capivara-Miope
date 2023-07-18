using UnityEngine;

public class QuestController : MonoBehaviour
{
    public QuestList allQuestsInGame;
    public QuestList activeQuests;
    public GameManager gameManager;
    private int[,] questsToReturn = {{0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}};
    private int c = 0;

    public void GainQuest(QuestContainer newQuest)
    {
        activeQuests.GetNewQuest(newQuest);
    }

    public void AdvanceQuest(QuestContainer actualQuest)
    {
        if (activeQuests.AdvanceStage(actualQuest))
        {
            activeQuests.CompleateQuest(actualQuest);
            gameManager.coinBag.AddCoins(actualQuest.coinReward);
            //gameManager.crafting.VerifyIfItsKnownRecipe(actualQuest.rewardRecipe);
            foreach (CraftingRecipe craftingRecipe in actualQuest.rewardRecipes)
            {
                gameManager.crafting.VerifyIfItsKnownRecipe(craftingRecipe);
            }
        }
        else
        {
            return;
        }
    }

    public QuestList LoadQuests(int[,] quests)
    {
        for (int i = 0; i < quests.GetLength(0); i++)
        {
            if (allQuestsInGame.quests[i].questId.Equals(quests[i, 0]))
            {
                activeQuests.LoadOwnedQuest(allQuestsInGame.quests[i], quests[i, 1]);
                //Printando informação
                print("Quest Ativa: " + allQuestsInGame.quests[i].questName);
            }
        }
        return activeQuests;
    }

    public int[,] GetActiveQuests()
    {
        c = 0;
        foreach (QuestContainer quest in activeQuests.quests)
        {
            questsToReturn[c, 0] = quest.questId;
            questsToReturn[c, 1] = quest.actualStage;
            c++;
        }
        return questsToReturn;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            print(activeQuests.GetStageObjective(activeQuests.quests[0]));
        }
    }

}
