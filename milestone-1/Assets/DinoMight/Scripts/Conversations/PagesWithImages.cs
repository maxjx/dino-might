using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PagesWithImages : DialogueWithPages
{
    public Image[] images;
    private int imageIndex;

    // next sentence is also next image, if sentences less than images, show last sentence, if images less than sentence, dont show image
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
            images[index].gameObject.SetActive(true);
        }
        else if (imagesLen > 0)         // index is greater tha images length ...
        {
            images[index].gameObject.SetActive(false);
        }

        index++;

        if (index == sentencesLen && index == imagesLen)
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
}
