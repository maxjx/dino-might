using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// now used to show things according to quest number
public class QuestPrompt : MonoBehaviour
{
    public List<int> taskNumbers;       // List of task numbers that make the quest prompt show
    public bool isImage = false;          // whether image or sprite
    public bool isButton = false;

    private int questNumber;
    private Animator animator;
    private Image image;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        questNumber = Global.questNumber;
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
        button = GetComponent<Button>();

        if (taskNumbers.Contains(questNumber))     // questNumber = 0 returns true
        {
            ShowQuestPrompt();
        }
        else
        {
            HideQuestPrompt();
        }
    }

    void ShowQuestPrompt()
    {
        if (isImage)
        {
            image.enabled = true;
        }
        else
        {
            animator.SetBool("visible", true);
        }

        if (isButton)
        {
            button.enabled = true;
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void HideQuestPrompt()
    {
        if (isImage)
        {
            image.enabled = false;
        }
        else
        {
            animator.SetBool("visible", false);
        }

        if (isButton)
        {
            button.enabled = false;
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
