using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionPanelController : MonoBehaviour
{
    [Header("Essential & Resources")]
    #region 
    private bool opened = false;
    public QuestList playerQuests;
    public GameObject missionPanel;
    public GameManager gameManager;
    public TextMeshProUGUI txtPageNumber;
    #endregion

    [Header("Quest Details")]
    #region
    public GameObject detailsPanel;
    public TextMeshProUGUI txtTitleQuest;
    public TextMeshProUGUI txtDescription;
    public TextMeshProUGUI txtObjective;
    #endregion
    

    

    [Header("Show Quests")]
    #region
    public List<QuestButtonsControl> buttons;
    private int questCount = 0;
    public int firstQuestShown = -1;
    public int lastQuestShown = -1;
    private int c = 0;
    private int page = 0;
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (!opened)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
    }
    
    private void Open()
    {
        detailsPanel.SetActive(false);
        page = 0;
        opened = true;
        firstQuestShown = 5 * page;
        if (playerQuests.quests.Count >= 4)
        {
            lastQuestShown = firstQuestShown + 4;
        }
        else
        {
            lastQuestShown = playerQuests.quests.Count - 1;
        }
        missionPanel.SetActive(true);
        gameManager.ControlCharacterControls(false, false);
        ChangePage(0);
    }

    public void Close()
    {
        opened = false;
        missionPanel.SetActive(false);
        gameManager.ControlCharacterControls(true, true);
    }

    private void Show()
    {
        foreach (QuestButtonsControl cbt in buttons)
        {
            cbt.Disable();
        }
        
        firstQuestShown = 5 * page;
        lastQuestShown = firstQuestShown + 4;
        c = firstQuestShown;
        if (lastQuestShown >= playerQuests.quests.Count)
        {
            lastQuestShown = playerQuests.quests.Count - 1;
        }
        for (int i = 0; i < buttons.Count; i++)
        {
            if (c <= lastQuestShown)
            {
                buttons[i].Set(playerQuests.quests[c], c);
            }
            else
            {
                buttons[i].Disable();
            }
            c++;
        }
        c = 0;
    }
    
    public void ChangePage(int c)
    {
        if (firstQuestShown + c >= 0 && lastQuestShown + c <= playerQuests.quests.Count - 1)
        {
            page = page + c;
            Show();
            txtPageNumber.text = (page + 1) + "/" + (Mathf.Round(playerQuests.quests.Count/5) + 1);
        }
    }

    public void ShowQuestDetails(int index)
    {
        detailsPanel.SetActive(true);
        txtTitleQuest.text = playerQuests.quests[index].questName;
        txtDescription.text = playerQuests.quests[index].questDescription;
        txtObjective.text = playerQuests.GetStageObjective(playerQuests.quests[index]);
    }
    
}
