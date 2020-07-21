using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager manager;
    public GameObject prompt;
    public string NPCName;
    public bool autoTrigger = false;        // For making dialogues with ownself, still have to go into collider
    private bool autoTriggered = false;
    private bool talking = false;

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
            // if the correct trigger keys are pressed
            else if (TriggerKeysPressed()) // and not autotrigger
            {
                prompt.SetActive(false);
                TriggerDialogue();
            }
            // else is not an autotrigger nor c was pressed
        }
    }

    public bool TriggerKeysPressed()
    {
        return Input.GetKeyDown("c");
    }

    void TriggerDialogue()
    {
        manager.ToggleDisplayName(NPCName);
        manager.StartDialogue(this);
        talking = true;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && (talking == false))
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

    // when talking is done and want to show the prompt again,
    public void TurnOnPrompt()
    {
        talking = false;
    }
    public void TurnOffPrompt()
    {
        talking = true;
        prompt.SetActive(false);
    }
}
