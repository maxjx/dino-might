using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public GameObject prompt;
    [TextArea(3, 5)]
    public string[] sentences;
    public float typingSpeed = 0.01f;

    private int index = 0;
    private bool conversing;    // Player is in NPC's area and triggered prompt

    void Start()
    {
        prompt.SetActive(false);
    }

    void Update()
    {
        if (conversing && Input.GetKeyDown("c"))
        {
            prompt.SetActive(false);
            NextSentence();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            prompt.SetActive(true);
            conversing = true;
            index = 0;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        prompt.SetActive(false);
    }

    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void NextSentence()
    {
        if (index < sentences.Length)
        {
            textDisplay.text = "";
            StartCoroutine(Type());
            index++;
        }
        else    // End of conversation
        {
            textDisplay.text = "";
        }
    }
}
