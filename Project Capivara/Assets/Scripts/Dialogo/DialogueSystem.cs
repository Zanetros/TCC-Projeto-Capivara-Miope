using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image portrait;
    [SerializeField] AudioClip dialogueSound;

    private DialogueContainer currentDialogue;
    private int currentLine, letterCount;

    [Range(0f,1f)]
    [SerializeField] private float visibleTextPercent;
    
    [SerializeField] private float timePerLetter = 0.05F;
    private float totalTimeToType, currentTime;
    private string lineToShow;

    public GameManager gameManager;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PushText();
        }
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

    private void PushText()
    {
        if (visibleTextPercent < 1F)
        {
            visibleTextPercent = 1F;
            UpdateText();
            return;
        }
        if (currentLine >= currentDialogue.line.Count)
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
        lineToShow = currentDialogue.line[currentLine];
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
        AudioManager.instance.Play(dialogueSound);
        currentDialogue = dialogueContainer;
        visibleTextPercent = 1;
        currentLine = 0;
        targetText.text = "";
        UpdatePortrait();
        PushText();
        gameManager.playerMovement.enabled = false;
    }

    private void UpdatePortrait()
    {
        portrait.sprite = currentDialogue.actor.portrait;
        nameText.text = currentDialogue.actor.name;
    }

    private void Show(bool b)
    {
        Debug.Log("cOMECOU DIALOGO");
        gameManager.playerMovement.enabled = b;
        gameManager.characterInteractController.enabled = b;
        gameObject.SetActive(b);
    }

    void Conclude()
    {
        AudioManager.instance.Stop(dialogueSound);
        print("The dialogue has ended");
        Show(false);
        gameManager.characterInteractController.enabled = true;
        gameManager.playerMovement.enabled = true;       
    }
}
