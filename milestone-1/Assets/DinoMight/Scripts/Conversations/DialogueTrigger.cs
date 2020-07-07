using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager manager;
    public GameObject prompt;
    public string NPCName;
    public bool autoTrigger = false;        // For making dialogues with ownself
    private bool autoTriggered = false;

    void Start()
    {
        prompt.SetActive(false);
    }

    void Update()
    {
        // If prompt is active (Player is in collider),
        if (prompt.activeSelf)
        {
            if (autoTrigger)
            {
                if (!autoTriggered)         // not autoTriggered yet
                {
                    prompt.SetActive(false);
                    TriggerDialogue();
                    autoTriggered = true;
                }
            }
            else if (Input.GetKeyDown("c")) // and not autotrigger
            {
                prompt.SetActive(false);
                TriggerDialogue();
            }
            // else is not an autotrigger nor c was pressed
        }
    }

    void TriggerDialogue()
    {
        manager.StartDialogue();
        manager.ToggleDisplayName(NPCName);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            prompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            prompt.SetActive(false);
        }
    }

    public void TurnOnPrompt()
    {
        prompt.SetActive(true);
    }
}
