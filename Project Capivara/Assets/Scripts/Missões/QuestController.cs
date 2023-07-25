using UnityEngine;

public class QuestController : MonoBehaviour
{
    //public QuestList allQuestsInGame;
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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            print(activeQuests.GetStageObjective(activeQuests.quests[0]));
        }
    }

}
