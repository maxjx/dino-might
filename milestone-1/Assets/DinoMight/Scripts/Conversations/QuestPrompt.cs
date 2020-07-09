using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPrompt : MonoBehaviour
{
    public List<int> taskNumbers;       // List of task numbers that make the quest prompt show
    private int questNumber;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        questNumber = Global.questNumber;
        animator = GetComponent<Animator>();

        if (taskNumbers.Contains(questNumber))     // questNumber = 0 returns true
        {
            ShowQuestPrompt();
        }
    }

    void ShowQuestPrompt()
    {
        animator.SetBool("visible", true);
    }

    public void HideQuestPrompt()
    {
        animator.SetBool("visible", false);
    }
}
