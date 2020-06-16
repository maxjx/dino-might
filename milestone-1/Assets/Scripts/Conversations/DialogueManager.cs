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

    private GameObject player;
    private playerMovement pm;
    private Attack a;
    private PlayerHealth h;
    private bool dialogueStarted;
    private bool nameDisplayed = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pm = player.GetComponent<playerMovement>();
        a = player.GetComponent<Attack>();
        h = player.GetComponent<PlayerHealth>();
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
        a.enabled = !a.enabled;
        h.enabled = !h.enabled;
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
