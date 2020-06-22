using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider fill;
    public Gradient gradient;
    public Image fillColor;
    public Text value;
    private int maxHealth;

    public void setMaxHealth(int health) {
        maxHealth = health;
        fill.maxValue = health;
        fill.value = health;
        fillColor.color = gradient.Evaluate(1f);
        value.text = health.ToString() + " / " + health.ToString();
    }

    public void setHealth(int health) {
        fill.value = health;
        fillColor.color = gradient.Evaluate(fill.normalizedValue);
        value.text = health.ToString() + " / " + maxHealth.ToString();
    }
}
