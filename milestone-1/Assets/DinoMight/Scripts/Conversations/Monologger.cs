using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Monologger : MonoBehaviour
{
    public string[] sentences;
    public float sentenceDuration = 3f;     // Time from current sentence completely typed out until the next sentence is typed out
    public float typingSpeed = 0.02f;
    public TextMeshProUGUI textbox;
    public Animator MonologueBackgroundAnim;
    private int index = 0;
    private bool played = false;        // Monologue has been played

    // Triggered by another object when a specific event happens such as mob dying
    public void ManualTrigger()
    {
        if (!played)
        {
            MonologueBackgroundAnim.SetTrigger("entry");
            played = true;
            StartCoroutine(PlayMonologue());
        }
    }

    // Triggers monologue when player steps into this collider
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!played && collider.CompareTag("Player"))
        {
            MonologueBackgroundAnim.SetTrigger("entry");
            played = true;
            StartCoroutine(PlayMonologue());
        }
    }

    private IEnumerator PlayMonologue()
    {
        while (index < sentences.Length)
        {
            textbox.text = "";
            yield return StartCoroutine(Type());
            yield return new WaitForSeconds(sentenceDuration);
            index++;
        }
        textbox.text = "";
        MonologueBackgroundAnim.SetTrigger("exit");
    }

    private IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textbox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
