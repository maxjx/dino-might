using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Monologger : MonoBehaviour
{
    public string[] sentences;
    public float sentenceDuration = 3f;
    public float typingSpeed = 0.02f;
    public TextMeshProUGUI textbox;
    public Animator MonologueBackgroundAnim;
    private int index = 0;
    private bool played = false;        // Monologue has been played

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
