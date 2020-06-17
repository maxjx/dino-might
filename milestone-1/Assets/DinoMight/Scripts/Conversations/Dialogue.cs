using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public DialogueManager manager;
    public float typingSpeed = 0.01f;
    [TextArea(3, 5)]
    public string[] sentences;
    public Button[] choices;
    private TextMeshProUGUI textBox;
    private int index = 0;
    private bool conversing = false;
    private bool typing;

    void Start()
    {
        textBox = GetComponent<TextMeshProUGUI>();
        HideChoices();
    }

    void Update()
    {
        if (!typing && conversing && Input.anyKeyDown)
        {
            manager.currentDialogue = this;
            StartCoroutine(NextSentenceCoroutine());
        }
    }

    // Is accessed by DialogueTrigger and Buttons
    public void NextSentence()
    {
        StartCoroutine(NextSentenceCoroutine());
    }

    private IEnumerator NextSentenceCoroutine()
    {
        if (index < sentences.Length)
        {
            conversing = true;
            textBox.text = "";
            yield return StartCoroutine(Type());    // Waits for typing to finish
            index++;
        }
        else   // If last sentence was displayed
        {
            if (choices.Length == 0)    // This is the last dialogue
            {
                textBox.text = "";
                manager.EndDialogues();
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

    public void HideChoices()
    {
        foreach (Button choice in choices)
        {
            choice.gameObject.SetActive(false);
        }
    }

    private void DisplayChoices()
    {
        foreach (Button choice in choices)
        {
            choice.gameObject.SetActive(true);
        }
    }

    public void ClearText()
    {
        textBox.text = "";
    }
}
