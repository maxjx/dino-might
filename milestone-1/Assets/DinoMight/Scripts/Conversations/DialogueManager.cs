using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Dialogue currentDialogue;    // Insert intro dialogue here which will lead to all the other dialogues
    public GameObject NPCCamera;
    public Animator dialogueBackground;
    public TextMeshProUGUI nameBox;     // TMPro textbox for name
    public GameObject player;
    
    private playerMovement pm;
    private Attack attack;
    private PlayerHealth health;
    private bool immune = false;        // to toggle health
    private bool dialogueStarted;
    private bool nameDisplayed = false;

    void Start()
    {
        pm = player.GetComponent<playerMovement>();
        attack = player.GetComponent<Attack>();
        health = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (dialogueStarted && Input.GetButtonDown("Cancel"))
        {
            EndDialogues();
        }
    }

    public void StartDialogue()
    {
        // For exiting conversation from Update
        dialogueStarted = true;

        dialogueBackground.SetTrigger("entry");

        // Pan camera into NPC view
        NPCCamera.SetActive(true);

        // Disables/Enables player
        ToggleEnablePlayer();
        currentDialogue.NextSentence();
    }

    void ToggleEnablePlayer()
    {
        pm.ToggleStartStopMovement();
        attack.enabled = !attack.enabled;
        immune = !immune;
        health.Immunity(immune);
    }

    public void EndDialogues()
    {
        dialogueStarted = false;

        // clear dialogue box
        currentDialogue.ClearText();
        currentDialogue.HideChoices();
        ToggleDisplayName("");          // Blank for NPC name, should be ending dialogue anyway
        
        dialogueBackground.SetTrigger("exit");

        // Pan camera back to normal view
        NPCCamera.SetActive(false);
        ToggleEnablePlayer();
    }

    public void ToggleDisplayName(string NPCName)
    {
        if (nameDisplayed)
        {
            nameBox.text = "";
            nameDisplayed = false;
        }
        else
        {
            nameBox.text = NPCName;
            nameDisplayed = true;
        }
    }
}
