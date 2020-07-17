using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Recording this type of dialogue not implemented
public class DialogueWithPages : Dialogue
{
    public Button[] nextDialogueChoices;       // choice that leads to the next dialogue
    protected int nextDialogueChoiceIndex = 0;     // index to choose the next dialogue button which can lead to diff endings
    public bool timedFinish = false;                // changes finish button based on duration stayed in this dialogue
    public float timeRemaining = 180f;

    protected override void Update()
    {
        if (timedFinish)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                nextDialogueChoiceIndex = 1;
            }
        }
    }

    // start any dialogue with next sentence, so choices will always be displayed for this dialoguewithpages
    public override void NextSentence()
    {
        DisplayChoices();

        if (index < sentences.Length)
        {
            textBox.text = sentences[index];
            index++;
        }

        if (index == sentences.Length)
        {
            DisplayNextDialogueChoice();
        }
        // {
        //     EndDialogue();
        //     nextDialogue.NextSentence();
        // }
    }

    public virtual void PreviousSentence()
    {
        if (index == sentences.Length)
        {
            UndisplayNextDialogueChoice();
        }
        index -= 2;
        if (index < 0)
        {
            index = 0;
        }
        NextSentence();
    }

    // disables the last choice on the list and enables the next dialogue choice in its place
    protected void DisplayNextDialogueChoice()
    {
        if (choices.Length > 0)
        {
            choices[choices.Length - 1].gameObject.SetActive(false);
        }
        nextDialogueChoices[nextDialogueChoiceIndex].gameObject.SetActive(true);
    }

    protected void UndisplayNextDialogueChoice()
    {
        if (choices.Length > 0)
        {
            choices[choices.Length - 1].gameObject.SetActive(true);
        }
        // set any next dialogue choice to be false
        foreach (Button NDChoice in nextDialogueChoices)
        {
            NDChoice.gameObject.SetActive(false);
        }
    }

    public override void HideChoices()
    {
        foreach (Button choice in choices)
        {
            choice.gameObject.SetActive(false);
        }
        // set any next dialogue choice to be false
        foreach (Button NDChoice in nextDialogueChoices)
        {
            NDChoice.gameObject.SetActive(false);
        }
    }

    public override void EndDialogue()
    {
        HideChoices();
        textBox.text = "";
    }
}
