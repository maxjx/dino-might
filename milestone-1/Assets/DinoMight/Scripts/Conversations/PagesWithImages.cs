using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PagesWithImages : DialogueWithPages
{
    public Image[] images;
    private int imageIndex;

    // next sentence is also next image, if sentences less than images, show last sentence, vice versa
    public override void NextSentence()
    {
        int sentencesLen = sentences.Length;
        int imagesLen = images.Length;

        DisplayChoices();

        if (index < sentencesLen)
        {
            textBox.text = sentences[index];
        }
        else if (sentencesLen > 0)      // index is greater than sentences length and sentences length is not 0
        {
            textBox.text = sentences[sentencesLen - 1];
        }

        if (index < imagesLen)
        {
            if (index > 0)
            {
                images[index - 1].gameObject.SetActive(false);
            }
            if (index + 1 < imagesLen)
            {
                images[index + 1].gameObject.SetActive(false);
            }
            images[index].gameObject.SetActive(true);
        }
        else if (imagesLen > 0)         // index is greater tha images length ...
        {
            images[imagesLen - 1].gameObject.SetActive(true);
        }

        index++;

        if (index >= sentencesLen && index >= imagesLen)
        {
            DisplayNextDialogueChoice();
        }
    }

    public override void PreviousSentence()
    {
        if (index >= sentences.Length && index >= images.Length)        // index larger or equal to sentences and index length, means end of dialogue
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

    public override void EndDialogue()
    {
        HideChoices();
        textBox.text = "";
        if (images.Length > 0)
        {
            images[images.Length - 1].gameObject.SetActive(false);
        }
    }
}
