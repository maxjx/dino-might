using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Monologger : MonoBehaviour
{
    public string id;                   // Can be any name as long as it is unique to all monologgers, used to record monologue in Global
    public string[] sentences;
    public float sentenceDuration = 3f;     // Time from current sentence completely typed out until the next sentence is typed out
    public float typingSpeed = 0.02f;
    public TextMeshProUGUI textbox;
    public Animator MonologueBackgroundAnim;
    public bool replayable = false;
    [Space]
    public UnityEvent endingThings;

    private int index = 0;
    private bool played = false;        // Monologue has been played

    void Start()
    {
        if (endingThings == null)
            endingThings = new UnityEvent();
        if (!replayable)
        {
            // Check Global if played if not replayable. else played is always false.
            if (Global.playedMonologueList.Contains(id))
            {
                played = true;
            }
        }
    }

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
        int sentencesLen = sentences.Length;
        while (index < sentencesLen)
        {
            // if replayable, dont have to record
            if (!replayable && index == sentencesLen - 1)
            {
                // It is the last sentence so record it to Global even if monologue is prematurely ended
                Global.playedMonologueList.Add(id);
            }
            textbox.text = "";
            yield return StartCoroutine(Type());
            yield return new WaitForSeconds(sentenceDuration);
            index++;
        }
        textbox.text = "";
        MonologueBackgroundAnim.SetTrigger("exit");
        index = 0;
        endingThings.Invoke();
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
