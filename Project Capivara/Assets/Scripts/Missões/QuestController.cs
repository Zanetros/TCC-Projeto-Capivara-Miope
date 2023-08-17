using UnityEngine;

public class QuestController : MonoBehaviour
{
    //public QuestList allQuestsInGame;
    public QuestList activeQuests;
    public QuestList compleatedQuests;
    public GameManager gameManager;
    private int[,] questsToReturn = {{0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}};
    private int c = 0;

    public void GainQuest(QuestContainer newQuest)
    {
        activeQuests.GetNewQuest(newQuest);
    }

    public bool VerifyQuestToGain(QuestContainer quest)
    {
        foreach (QuestContainer questCompleated in compleatedQuests.quests)
        {
            if (questCompleated.Equals(quest))
            {
                return true;
            }
        }
        return false;
    }
    
    public DialogueByDay AdvanceQuest(QuestContainer actualQuest)
    {
        //Caso a quest tenha sido completada
        if (activeQuests.AdvanceStage(actualQuest))
        {
            gameManager.questController.activeQuests.CompleateQuest(actualQuest);
            gameManager.questController.compleatedQuests.quests.Add(actualQuest);
            gameManager.coinBag.AddCoins(actualQuest.coinReward);
            foreach (CraftingRecipe craftingRecipe in actualQuest.rewardRecipes)
            {
                gameManager.crafting.VerifyIfItsKnownRecipe(craftingRecipe);
            }
        }
        return actualQuest.stagesCompleatedDialogue[actualQuest.actualStage-1];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            print(activeQuests.GetStageObjective(activeQuests.quests[0]));
        }
    }

    public bool VerifyItemQuestToAdvance(QuestContainer questContainer, ItemContainer playerInventory)
    {
        if (questContainer.actualStage >= questContainer.stages.Count || questContainer.compleated)
        {
            return false;
        }
        foreach (ItemSlot item in questContainer.stages[questContainer.actualStage].itensToReceive)
        {
            if (!playerInventory.CheckItemForQuantity(item, item.count))
            {
                print("Sem todos os itens do estágio atual da quest");
                return false;
            }
        }
        return true;
    }

}