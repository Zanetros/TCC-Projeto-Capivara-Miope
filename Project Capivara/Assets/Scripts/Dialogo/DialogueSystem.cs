using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [Header("Campos")]
    #region
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image portrait;
    [SerializeField] private GameObject inventoryDialoguePanel;
    #endregion

    [Header("Text Variables")]
    #region
    public DialogueContainer currentDialogueContainer;
    public DialogueByDay currentDialogue;
    private int currentLine, letterCount;
    [Range(0f,1f)]
    [SerializeField] private float visibleTextPercent;
    [SerializeField] private float timePerLetter = 0.05F;
    private float totalTimeToType, currentTime;
    private string lineToShow;
    public List<DialogueInventoryButtonController> buttons;
    private ItemContainer itensInLocalInventory;
    private int giftCount = 0;
    private bool advanceBlocked = false;
    public GameObject dialogueCanvas;
    #endregion

    [Header("Trade Variables")]
    #region
    public TextMeshProUGUI txtItemName;
    public GameObject btnGiveItem;
    public DialogueInventoryButtonController selectedSlotController;
    public GameObject btnAdvanceQuest;
    private QuestContainer questToAdvance = null;
    private int d = 0;
    #endregion

    [Header("Other Scripts")]
    #region
    public GameManager gameManager;
    public AudioManager audioManager;
    public NpcWalkController npcTalking;
    #endregion
    
    void Update()
    {
        TypeOutText();
    }

    private void TypeOutText()
    {
        if (visibleTextPercent >= 1F) { return;}
        currentTime += Time.deltaTime;
        visibleTextPercent = currentTime / totalTimeToType;
        visibleTextPercent = Mathf.Clamp(visibleTextPercent, 0 , 1F);
        UpdateText();
    }

    void UpdateText()
    {
        if (lineToShow == null) return;
        letterCount = (int)(lineToShow.Length * visibleTextPercent);
        targetText.text = lineToShow.Substring(0, letterCount);
    }

    public void PushText()
    {
        if (advanceBlocked)
        {
            return;
        }
        if (visibleTextPercent < 1F)
        {
            visibleTextPercent = 1F;
            UpdateText();
            return;
        }
        if (currentLine >= currentDialogue.lines.Count)
        {
            if (currentDialogue.questToAdd == null || gameManager.questController.activeQuests.quests.Contains(currentDialogue.questToAdd))
            {
                Conclude();   
            }
            else
            {
                gameManager.questController.GainQuest(currentDialogue.questToAdd);
                Initialize(currentDialogueContainer, null, true, false);
            }
        }
        else
        {
         CycleLine();   
        }
    }

    void CycleLine()
    {
        lineToShow = currentDialogue.lines[currentLine];
        totalTimeToType = lineToShow.Length * timePerLetter;
        currentTime = 0F;
        visibleTextPercent = 0F;
        letterCount = 0;
        targetText.text = "";
        currentLine++;
    }
    
    public void Initialize(DialogueContainer dialogueContainer, DialogueByDay questDialog, bool isToGetQuest, bool isFromQuestAdvanced)
    {
        Show(true);
        gameManager.ControlCharacterControls(false, false);
        AudioManager.instance.Play(AudioManager.instance.dialogo);
        if (isFromQuestAdvanced)
        {
            currentDialogue = questDialog;
        }
        else
        {
            currentDialogueContainer = dialogueContainer;  
            if (!isToGetQuest)
            {
                currentDialogue = dialogueContainer.lineInEachDay[gameManager.timeManager.GetDay() - 1];   
            }
            else
            {
                currentDialogue = dialogueContainer.lineInEachDay[gameManager.timeManager.GetDay() - 1].questDialog;
            }
        }
        visibleTextPercent = 1;
        currentLine = 0;
        targetText.text = "";
        UpdatePortrait(dialogueContainer);
        PushText();
        if (VerifyQuestForNPC())
        {
               btnAdvanceQuest.SetActive(true);
        }
        else
        {
            btnAdvanceQuest.SetActive(false);
        }
    }

    private bool VerifyQuestForNPC()
    {
        foreach (QuestContainer quest in gameManager.questController.activeQuests.quests)
        {
            if ( currentDialogueContainer != null && quest.questActor.Equals(currentDialogueContainer.actor))
            {
                if (gameManager.questController.VerifyItemQuestToAdvance(quest, gameManager.inventoryContainer))
                {
                    questToAdvance = quest;
                    return true;
                }
                return false;
            }
        }
        return false;
    }
    
    public void AdvanceQuest()
    {
        if (questToAdvance != null)
        {
            foreach (ItemSlot item in gameManager.inventoryContainer.slots)
            {
                if (item.Equals(questToAdvance.stages[questToAdvance.actualStage].itensToReceive))
                {
                    gameManager.inventoryContainer.Remove(item.item, 
                        questToAdvance.stages[questToAdvance.actualStage].itensToReceive[d].count);
                    d++;
                }
            }
            Initialize(currentDialogueContainer, gameManager.questController.AdvanceQuest(questToAdvance), false, true);
            gameManager.questController.activeQuests.CompleateQuest(questToAdvance);
            questToAdvance = null;
        }
        d = 0;
    }
    
    private void UpdatePortrait(DialogueContainer dialogueContainer)
    {
        portrait.sprite = dialogueContainer.actor.portrait;
        nameText.text = dialogueContainer.actor.name;
    }

    private void Show(bool b)
    {
        dialogueCanvas.SetActive(b);
    }

    void Conclude()
    {
        if (!npcTalking.Equals(null))
        {
            npcTalking.talking = false;
            npcTalking = null;
        }
        AudioManager.instance.Stop(AudioManager.instance.dialogo);
        Show(false);
        gameManager.ControlCharacterControls(true, true);
        gameManager.ControlPassageOfTime(false);
    }
    
    public void ShowItensToGive()
    {
        advanceBlocked = true;
        itensInLocalInventory = gameManager.inventoryController.itemContainer;
        foreach (DialogueInventoryButtonController dibc in buttons)
        {
            dibc.Clear();
        }
        for (int i = 0; i < gameManager.inventoryController.itemContainer.slots.Count; i++)
        { 
            if (itensInLocalInventory.slots[i].item != null && itensInLocalInventory.slots[i].item.isGift)
            {
                buttons[giftCount].Set(itensInLocalInventory.slots[i], i, false);
                giftCount++;   
            }
        }
        giftCount = 0;
        inventoryDialoguePanel.SetActive(true);
        txtItemName.text = "";
    }

    public void CloseInventoryItens()
    {
        advanceBlocked = false;
        inventoryDialoguePanel.SetActive(false);
        btnGiveItem.gameObject.SetActive(false);
        selectedSlotController.Clear();
    }

    public void GetItemToGive(ItemSlot itemSlot, int index)
    {
        selectedSlotController.Set(itemSlot, index, false);
        btnGiveItem.gameObject.SetActive(true);
    }

    public void DeselectItem()
    {
        btnGiveItem.gameObject.SetActive(false);
        txtItemName.text = "";
    }
    
}
