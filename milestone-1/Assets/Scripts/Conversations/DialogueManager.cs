using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Dialogue currentDialogue;
    public GameObject NPCCamera;

    private GameObject player;
    private playerMovement pm;
    private Attack a;
    private Health h;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pm = player.GetComponent<playerMovement>();
        a = player.GetComponent<Attack>();
        h = player.GetComponent<Health>();
    }

    public void NextDialogue()
    {
        NPCCamera.SetActive(true);
        ToggleEnablePlayer();
        currentDialogue.NextSentence();
    }

    void ToggleEnablePlayer()
    {
        pm.enabled = !pm.enabled;
        a.enabled = !a.enabled;
        h.enabled = !h.enabled;
    }

    public void EndDialogues()
    {
        NPCCamera.SetActive(false);
        ToggleEnablePlayer();
    }
}
