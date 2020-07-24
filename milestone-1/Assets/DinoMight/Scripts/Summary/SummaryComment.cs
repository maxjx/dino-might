﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SummaryComment : MonoBehaviour
{
    private TextMeshProUGUI textbox;

    // Start is called before the first frame update
    void Start()
    {
        textbox = GetComponent<TextMeshProUGUI>();
        textbox.text = FindComment(GetTotalCorrectChoices());
    }

    string FindComment(float ratio)
    {
        if (ratio == 1f)
        {
            return "[Perfect ending] Looks like you have helped Dino make the right decisions and Dino is now stress-free! Congratulations!";
        }
        else if (ratio <= 1/3f)
        {
            return "[Bad ending] Dino walks out of the forest feeling confused and tired. "
                   + "He realised that he still feels stress and frustrated… Looks like you need to pick up some better stress relief options!";
        }
        else
        {
            return "[Good ending] Dino walks out of the forest feeling much better than before, while some decisions could be better, most of them were great! "
                   + "Keep up the good job!";
        }
    }

    float GetTotalCorrectChoices()
    {
        return (Global.priorities.Count + Global.challenges.Count + Global.habits.Count) / 6;
    }
}
