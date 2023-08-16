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

    public DialogueByDay AdvanceQuest(QuestContainer actualQuest)
    {
        //Caso a quest tenha sido completada
        if (activeQuests.AdvanceStage(actualQuest))
        {
            gameManager.coinBag.AddCoins(actualQuest.coinReward);
            //gameManager.crafting.VerifyIfItsKnownRecipe(actualQuest.rewardRecipe);
            foreach (CraftingRecipe craftingRecipe in actualQuest.rewardRecipes)
            {
                gameManager.crafting.VerifyIfItsKnownRecipe(craftingRecipe);
            }
        }
        return actualQuest.stagesCompleatedDialogue[actualQuest.actualStage];
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