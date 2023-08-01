using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestButtonsControl : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] MissionPanelController missionPanelController;
    public int myIndex = -1;
    private bool selected = false;

    public void SetIndex(int index)
    {
        myIndex = index;
    }
    
    public void Set(QuestContainer quest, int index)
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<Button>().enabled = true;
        SetIndex(index);
        text.text = quest.questName;
    }
    
    public void Clear()
    {
        myIndex = -1;
        gameObject.GetComponent<Button>().enabled = false;
        if(text != null) text.gameObject.SetActive(false);
    }
    
    public void Disable()
    {
        myIndex = -1;
        gameObject.SetActive(false);
    }

    public void SelectQuest()
    {
        missionPanelController.ShowQuestDetails(myIndex);
    }
    
}
