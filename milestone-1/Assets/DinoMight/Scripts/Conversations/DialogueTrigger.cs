using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager manager;
    public GameObject prompt;
    public string NPCName;

    void Start()
    {
        prompt.SetActive(false);
    }

    void Update()
    {
        if (prompt.activeSelf && Input.GetKeyDown("c"))
        {
            prompt.SetActive(false);
            manager.StartDialogue();
            manager.ToggleDisplayName(NPCName);
        }
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
}
