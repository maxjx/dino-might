﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public GameObject questToggleButton;
    public TextMeshProUGUI questBoardText;

    private int questNumber;

    // Start is called before the first frame update
    void Start()
    {
        questNumber = Global.questNumber;

        if (questNumber != 0)
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
    // Quest text must be updated at the start of every scene and when quest is accepted (but is not updating when quest is done)
    // Global quest number updates when quest is accepted or quest is done
    public void UpdateQuestText()
    {
        questNumber = Global.questNumber;
        switch (questNumber)
        {
            case 1:
                // tutorial level
                questBoardText.text = "Follow the path towards the right for now and step past the tree at the end of the path.";
                break;
            case 2:
                // out of tutorial
                questBoardText.text = "Someone in town with the yellow '!' needs your help!";
                break;
            case 3:
                // king quest
                questBoardText.text = "Find the King of west to the west of the town!";
                break;
            case 4:
                // king defeated
                questBoardText.text = "Nice job defeating that! Find the wizard in town for more instructions to get home!";
                break;      //Old man tells you to go to the north east of the town to find your way home. Finally!
            case 5:
                // lvl 3 fin
                questBoardText.text = "Why am i back here?? This might be a bug, tell the game masters!";
                break;
            default:
                questBoardText.text = "Someone in town with the yellow '!' needs your help!";
                break;
        }
    }

    public void IncrementQuest()
    {
        questToggleButton.SetActive(true);
        Global.questNumber++;
        UpdateQuestText();
    }
}
