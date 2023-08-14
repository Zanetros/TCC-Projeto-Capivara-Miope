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
    private DialogueByDay currentDialogue;
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
            Conclude();
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
    
    public void Initialize(DialogueContainer dialogueContainer)
    {
        Show(true);
        AudioManager.instance.Play(AudioManager.instance.dialogo);
        currentDialogue = dialogueContainer.lineInEachDay[gameManager.timeManager.GetDay() - 1];
        visibleTextPercent = 1;
        currentLine = 0;
        targetText.text = "";
        UpdatePortrait(dialogueContainer);
        PushText();
        gameManager.ControlCharacterControls(false, true);
    }

    private void UpdatePortrait(DialogueContainer dialogueContainer)
    {
        portrait.sprite = dialogueContainer.actor.portrait;
        nameText.text = dialogueContainer.actor.name;
    }

    private void Show(bool b)
    {
        gameManager.ControlCharacterControls(b, b);
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
        print("The dialogue has ended");
        Show(false);
        gameManager.ControlCharacterControls(true, true);
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
    }

    public void CloseInventoryItens()
    {
        advanceBlocked = false;
        inventoryDialoguePanel.SetActive(false);
    }
    
}
