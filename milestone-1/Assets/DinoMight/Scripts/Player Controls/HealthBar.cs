using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Slider fill;
    public Gradient gradient;
    public Image fillColor;
    public Text value;
    private int currHealth;
    private int maxHealth;

    public void setMaxHealth(int health) {
        maxHealth = health;
        fill.maxValue = health;
        fill.value = 0;
        fillColor.color = gradient.Evaluate(1f);
        value.text = "0 / " + health.ToString();
    }

    public void setHealth(int health) {
        currHealth = maxHealth - health;
        fill.value = currHealth;
        fillColor.color = gradient.Evaluate(fill.normalizedValue);
        value.text = currHealth.ToString() + " / " + maxHealth.ToString();
    }

}
