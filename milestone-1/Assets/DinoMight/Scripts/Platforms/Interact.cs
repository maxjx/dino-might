using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class Interact : MonoBehaviour
{
    [SerializeField] private GameObject prompt;

    public bool autoTrigger = false;        // For making dialogues with ownself
    protected Collider2D player;

    protected void Start()
    {
        prompt.SetActive(false);
    }

    protected void Update()
    {
        // If prompt is active (Player is in collider), and either "c" is pressed or it is auto triggered,
        if (prompt.activeSelf && (Input.GetKeyDown("c") || autoTrigger))
        {
            prompt.SetActive(false);
            TriggerAction();
        }
    }

    protected virtual void TriggerAction()
    {
        return;
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            player = collider;
            prompt.SetActive(true);
        }
    }

    protected void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            prompt.SetActive(false);
        }
    }
}
