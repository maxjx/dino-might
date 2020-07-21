using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dialogue : MonoBehaviour
{
    public DialogueManager manager;
    public string altName;                // alternate name for 2 way conversation. Once written, all subsequent dialogues must be given altNames.
    public float typingSpeed = 0.01f;
    [TextArea(3, 5)]
    public string[] sentences;
    public Button[] choices;
    public Dialogue nextDialogue;                   // Only used if choices size == 0 and nextDialogue != null
    // public bool tutorial = false;                   // To toggle tutorial instruction
    // public ToggleActivation tutorialInstruction;     // "Press any key to continue"

    protected TextMeshProUGUI textBox;
    protected int index = 0;
    private bool conversing = false;
    private bool typing;
    private Coroutine typingcoroutine;      // reference used to stop this coroutine

    void Start()
    {
        textBox = GetComponent<TextMeshProUGUI>();
        HideChoices();
    }

    protected virtual void Update()
    {
        if (!typing && conversing && Input.anyKeyDown)
        {
            // if (Input.GetMouseButtonDown(0)
            //     || Input.GetMouseButtonDown(1)
            //     || Input.GetMouseButtonDown(2))
            //     return;

            StartCoroutine(NextSentenceCoroutine());
            //SelectChoiceWithKey();
        }
        else if (typing)
        {
            if (Input.anyKeyDown)
            {
                FinishSentence();
            }
        }
    }

    // Is accessed by DialogueTrigger and Buttons and flowing dialogues
    public virtual void NextSentence()
    {
        if (altName != "")
        {
            manager.SwitchName(altName);
        }
        StartCoroutine(NextSentenceCoroutine());
    }

    private IEnumerator NextSentenceCoroutine()
    {
        if (index < sentences.Length)
        {
            conversing = true;
            manager.UpdateDialogueRef(this);        // Tells manager that this is the latest dialogue
            textBox.text = "";
            typingcoroutine = StartCoroutine(Type());
            yield return typingcoroutine;    // Waits for typing to finish

            // // Show "press any key to continue"
            // if (tutorial && index == 0)
            // {
            //     yield return new WaitForSeconds(0.5f);
            //     tutorialInstruction.Activate();
            //     tutorial = false;
            // }

            index++;
        }
        else   // If last sentence was displayed
        {
            if (choices.Length == 0)    // This is the last dialogue
            {
                if (nextDialogue != null)
                {
                    this.EndDialogue();
                    nextDialogue.NextSentence();
                }
                else
                {
                    textBox.text = "";
                    manager.EndDialogues();
                }
            }
            else
            {
                DisplayChoices();
            }
            index = 0;  // To restart this Dialogue
            conversing = false;     // To ensure that this Dialogue only restarts by calling NextSentence outside this Dialogue
        }
    }

    private IEnumerator Type()
    {
        typing = true;
        foreach (char letter in sentences[index].ToCharArray())
        {
            textBox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        typing = false;
    }

    void FinishSentence()
    {
        StopCoroutine(typingcoroutine);

        if (index >= sentences.Length)
        {
            index = sentences.Length - 1;
        }
        textBox.text = sentences[index];
        index++;
        typing = false;
    }

    public virtual void HideChoices()
    {
        foreach (Button choice in choices)
        {
            choice.gameObject.SetActive(false);
        }
    }

    protected void DisplayChoices()
    {
        foreach (Button choice in choices)
        {
            choice.gameObject.SetActive(true);
        }
    }

    public virtual void EndDialogue()
    {
        conversing = false;
        StopCoroutine(NextSentenceCoroutine());
        StopCoroutine(typingcoroutine);
        HideChoices();
        index = 0;
        textBox.text = "";
    }

    protected void SelectChoiceWithKey()
    {
        if (choices != null && choices.Length != 0)
        {
            EventSystem.current.SetSelectedGameObject(null);
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                EventSystem.current.SetSelectedGameObject(choices[choices.Length - 1].gameObject);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
            }
        }
    }
}
