using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

// Controls the fill of the summary category bars.
public class SummaryCatBar : MonoBehaviour
{
    public int categoryNumber;      // 1: Prioritising, 2: Handling challenges, 3: Good habits
    public int maxValue = 2;            // number of questions for this category
    public Gradient gradient;
    public Image fillColor;
    public Text precentageTextBox;
    private Slider Sliderfill;
    private float updateSpeedSeconds = 0.5f;

    void Awake()
    {
        Sliderfill = GetComponent<Slider>();
    }

    void Start()
    {
        Sliderfill.maxValue = this.maxValue;
        Sliderfill.value = GetFillValue();
        StartCoroutine(FillUp(Sliderfill.value));
    }

    float GetFillValue()
    {
        switch (categoryNumber)
        {
            case 1:
                return Global.priorities.Count;
            case 2:
                return Global.challenges.Count;
            case 3:
                return Global.habits.Count;
            default:
                return Sliderfill.maxValue;
        }
    }
    
    IEnumerator FillUp(float totalValue)
    {
        float elapsed = 0f;
        float currentValue;
        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;

            currentValue = Mathf.Lerp(0f, totalValue, elapsed/updateSpeedSeconds);
            Sliderfill.value = currentValue;
            fillColor.color = gradient.Evaluate(Sliderfill.normalizedValue);
            precentageTextBox.text = Sliderfill.normalizedValue.ToString("P0", (CultureInfo)CultureInfo.InvariantCulture.Clone());

            yield return null;
        }

        Sliderfill.value = totalValue;
    }
}
