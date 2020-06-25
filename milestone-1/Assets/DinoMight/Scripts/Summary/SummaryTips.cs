using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SummaryTips : MonoBehaviour
{
    private TextMeshProUGUI textbox;

    // Start is called before the first frame update
    void Start()
    {
        textbox = GetComponent<TextMeshProUGUI>();
        textbox.text = "Tips:\n" + FindTip();
    }

    string FindTip()
    {
        string result = "";

        // Assume max value is 2 points
        if (Global.priorities < 2)
        {
            result += "Place your health first. Your mental wellbeing will thank you!\n\n";
        }
        else
        {
            result += "Good job on prioritising your activities. Your health should always come first!\n\n";
        }

        if (Global.challenges < 2)
        {
            result += "Don't let setbacks get you down. Learn from it and there will be better days!\n\n";
        }
        else
        {
            result += "You might be good at handling challenges, keep it up tough guy!\n\n";
        }

        if (Global.habits < 2)
        {
            result += "Regular sleeping and eating patterns are important to make your brain strong. Remember to get in some exercise too!";
        }
        else
        {
            result += "Regular sleeping and eating patterns are important to make your brain strong. Remember to get in some exercise too!";
        }

        return result;
    }
}
