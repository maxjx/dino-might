using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Dialogue currentDialogue;
    public GameObject NPCCamera;
    public Animator dialogueBackground;

    private GameObject player;
    private playerMovement pm;
    private Attack a;
    private Health h;
    private bool dialogueStarted;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pm = player.GetComponent<playerMovement>();
        a = player.GetComponent<Attack>();
        h = player.GetComponent<Health>();
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
        dialogueStarted = true;
        dialogueBackground.SetTrigger("entry");
        NPCCamera.SetActive(true);
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
        currentDialogue.ClearText();
        currentDialogue.HideChoices();
        dialogueBackground.SetTrigger("exit");
        NPCCamera.SetActive(false);
        ToggleEnablePlayer();
    }
}
