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
        textbox.text = FindTip();
    }

    string FindTip()
    {
        string result = "";

        // Assume max value is 2 points
        if (Global.priorities.Count < 2)
        {
            if (!Global.priorities.Contains("eatlunch"))
            {
                result += "Skipping meals is wrecks your body and mind! Use mealtime to relax and reflect on your day.\n";
            }
            if (!Global.priorities.Contains("planfuture"))
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
            if (!Global.challenges.Contains("ownup"))
            {
                result += "Talk to people about your problems to take things off your chest, and face setbacks with a positive attitude.\n";
            }
            if (!Global.challenges.Contains("journal"))
            {
                result += "A journal is a good friend in need when you are facing problems, or to organise your thoughts. Practise writing down your day and reflect on what was good or bad.\n";
            }
            result += "Embrace challenges as there will be better days!\n\n";
        }
        else
        {
            result += "You might be good at handling challenges, keep it up tough guy!\n\n";
        }

        if (Global.habits.Count < 2)
        {
            if (!Global.habits.Contains("workout"))
            {
                result += "Do create a positive habit of exercising. Even a short 10 minute stretching in the morning will make you feel so much better throughout the day and keep you performing tip top!\n";
            }
            if (!Global.habits.Contains("hwbreak"))
            {
                result += "Take a break! Remember to schedule breaks in between your work. The Pomodoro Technique calls for work sessions of 25mins followed by a 5min break, with a 15min break at least once every two hours.\n";
            }
            result += "Regular sleeping and eating patterns are important to make your brain strong!";
        }
        else
        {
            result += "Regular sleeping and eating patterns are important to make your brain strong. Remember to get in some exercise too!";
        }

        return result;
    }
}
