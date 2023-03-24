using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image portrait;

    private DialogueContainer currentDialogue;
    private int currentLine;

    [Range(0f,1f)]
    [SerializeField] private float visibleTextPercent;
    
    [SerializeField] private float timePerLetter = 0.05F;
    private float totalTimeToType, currentTime;
    private string lineToShow;
    
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
        int letterCount = (int)(lineToShow.Length * visibleTextPercent);
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
        targetText.text = "";
        currentLine++;
    }
    
    public void InitializeDialogue(DialogueContainer dialogueContainer)
    {
        Show(true);
        currentDialogue = dialogueContainer;
        currentLine = 0;
        PushText();
        UpdatePortrait();
    }

    private void UpdatePortrait()
    {
        portrait.sprite = currentDialogue.actor.portrait;
        nameText.text = currentDialogue.actor.name;
    }

    private void Show(bool b)
    {
        gameObject.SetActive(b);
    }

    void Conclude()
    {
        print("The dialogue has ended");
        Show(false);
    }
}