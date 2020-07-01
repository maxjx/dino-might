using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public GameObject questToggleButton;
    public TextMeshProUGUI questBoardText;
    public bool isInTown = false;

    private int questNumber;

    // Start is called before the first frame update
    void Start()
    {
        questNumber = Global.questNumber;

        if (questNumber != 0 || !isInTown)
        {
            ShowQuestButton();
            UpdateQuestText();
        }
    }

    public void ShowQuestButton()
    {
        questToggleButton.SetActive(true);
    }

    // 3 quests, 3 after-quests
    // Quest text must be updated at the start of every scene and when quest is accepted (but not when quest is done)
    // Global quest number updates when quest is accepted or quest is done
    public void UpdateQuestText()
    {
        questNumber = Global.questNumber;
        switch (questNumber)
        {
            case 1:
                // king quest
                questBoardText.text = "Your boss is calling you and he sounds angry!\nFind him down the path to the west of the town, all the way down!";
                break;
            case 2:
                // after king is defeated
                questBoardText.text = "Nice job defeating that! Find the old guy in town for more instructions to get home!";
                break;
            case 3:
                // level 3 temp quest
                questBoardText.text = "Old man tells you to go to the north east of the town to find your way home. Finally!";
                break;
            case 4:
                // lvl 3 fin
                questBoardText.text = "Why am i back here?? This might be a bug, tell the game masters!";
                break;
            default:
                questBoardText.text = "Someone in the town needs your help!";
                break;
        }
    }

    public void AcceptQuest()
    {
        questToggleButton.SetActive(true);
        Global.questNumber++;
        UpdateQuestText();
    }
}
