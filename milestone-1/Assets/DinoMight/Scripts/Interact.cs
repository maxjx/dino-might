using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour
{
    [SerializeField] private GameObject prompt;
    [SerializeField] private GameObject platform;
    public bool autoTrigger = false;        // For making dialogues with ownself

    void Start()
    {
        prompt.SetActive(false);
    }

    void Update()
    {
        // If prompt is active (Player is in collider), and either "c" is pressed or it is auto triggered,
        if (prompt.activeSelf && (Input.GetKeyDown("c") || autoTrigger))
        {
            prompt.SetActive(false);
            platform.GetComponent<MovingPlatformWithLock>().toggleLock();
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
