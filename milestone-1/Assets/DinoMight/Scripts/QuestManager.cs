using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public GameObject questToggleButton;
    public TextMeshProUGUI questBoardText;
    public GameObject questPrompt;
    private int questNumber;

    // Start is called before the first frame update
    void Start()
    {
        questNumber = Global.questNumber;
        if (questNumber == 0)
        {
            ShowQuestPrompt();
        }
        else
        {
            ShowQuestButton();
            UpdateQuestText();
        }
    }

    void ShowQuestPrompt()
    {
        questPrompt.SetActive(true);
    }

    public void ShowQuestButton()
    {
        questToggleButton.SetActive(true);
    }

    public void UpdateQuestText()
    {
        questNumber = Global.questNumber;
        switch (questNumber)
        {
            case 0:
                questBoardText.text = "Someone in the town needs your help!";
                break;
            case 1:
                questBoardText.text = "Your boss is calling you and he sounds angry!\nFind him down the path to the west of the town, all the way down!";
                break;
            case 2:
                questBoardText.text = "";
                break;
            default:
                break;
        }
    }

    public void AcceptQuest()
    {
        questToggleButton.SetActive(true);
        questPrompt.SetActive(false);
        Global.questNumber++;
        UpdateQuestText();
    }
}
