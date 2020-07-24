using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public GameObject questToggleButton;
    public TextMeshProUGUI questBoardText;

    private int questNumber;
    private List<string> quests = new List<string>(){
        "",     // quest button shouldnt appear yet
        "Follow the path towards the right for now and step past the tree at the end of the path.", // tutorial level
        "Someone in town with the yellow '!' needs your help!",                                     // out of tutorial
        "Find the King of west to the west of the town! Woowee, deeper into the forest~",           // king quest
        "Nice job defeating that! Find the wizard in town for more instructions to get home!",      // king defeated
        "Hone your skills in the upper lounge if you want to!",                                     // after giving W crown of time
        "Head south east towards the right side of the town and get through the magical caves to find the master in the south.", // 2nd arc
        "Great! Now get the loot back to the wizard!",   // after defeating master
        "Health is wealth. Get wealthy from the Inca ruins to the north!"      // last boss
    };

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
        questBoardText.text = quests[questNumber];
    }

    public void IncrementQuest()
    {
        questToggleButton.SetActive(true);
        Global.questNumber++;
        UpdateQuestText();
    }

    public void TempQuest(string description)
    {
        questBoardText.text = description;
    }

    // debugging purposes
    public void showTaskNum()
    {
        Debug.Log(Global.questNumber);
    }
}
