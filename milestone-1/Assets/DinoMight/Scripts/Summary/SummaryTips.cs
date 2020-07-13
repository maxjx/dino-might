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
        if (Global.priorities.Count < 2)
        {
            if (!Global.priorities.Contains("1eatlunch"))
            {
                result += "Skipping meals is wrecks your body and mind! Use mealtime to relax and reflect on your day.\n";
            }
            if (!Global.priorities.Contains("1planfuture"))
            {
                result += "Always have a goal in mind, be it short or long term. It keeps you focused.\n";
            }
            result += "Your mental wellbeing will thank you!\n\n";
        }
        else
        {
            result += "Good job on prioritising your activities. Your health should always come first!\n\n";
        }

        if (Global.challenges.Count < 2)
        {
            if (!Global.challenges.Contains("2ownup"))
            {
                result += "Talk to people about your problems to take things off your chest, and face setbacks with a positive attitude.\n";
            }
            // if (!Global.challenges.Contains("2"))
            // {
            //     result += "";
            // }
            result += "Embrace challenges as there will be better days!\n\n";
        }
        else
        {
            result += "You might be good at handling challenges, keep it up tough guy!\n\n";
        }

        if (Global.habits.Count < 2)
        {
            // if (!Global.habits.Contains("3workout"))
            // {
            //     result += "";
            // }
            // if (!Global.habits.Contains("3hwbreak"))
            // {
            //     result += "";
            // }
            result += "Regular sleeping and eating patterns are important to make your brain strong. Remember to get in some exercise too!";
        }
        else
        {
            result += "Regular sleeping and eating patterns are important to make your brain strong. Remember to get in some exercise too!";
        }

        return result;
    }
}
