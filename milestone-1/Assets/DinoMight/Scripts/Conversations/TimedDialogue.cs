using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimedDialogue : Dialogue
{
    public float timeRemaining = 120;        // in seconds
    [Space]
    public UnityEvent thingsThatHappenAfterTimesUp;

    private bool timerIsRunning = false;
    private string sentence = "Time remaining: ";            // display in front of timer

    // override so that choices can be shown and dialogue can lead to next dialogue
    public override void NextSentence()
    {
        timerIsRunning = true;
        base.DisplayChoices();
        if (base.sentences.Length > 0)
        {
            sentence = base.sentences[0];
        }
    }

    // override so that clicking and conversing dont matter
    protected override void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                EndDialogue();
                base.nextDialogue.NextSentence();

                if (thingsThatHappenAfterTimesUp == null)
                {
                    thingsThatHappenAfterTimesUp = new UnityEvent();
                }
                thingsThatHappenAfterTimesUp.Invoke();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        base.textBox.text = string.Format("{0}{1:00}:{2:00}", sentence, minutes, seconds);
    }

    public void Pause()
    {
        timerIsRunning = false;
    }

    public void Resume()
    {
        timerIsRunning = true;
    }

    public override void EndDialogue()
    {
        timeRemaining = 0;
        timerIsRunning = false;
        base.HideChoices();
        base.textBox.text = "";
    }
}
