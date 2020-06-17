using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Slider fill;
    public Gradient gradient;
    public Image fillColor;

    public void setMaxHealth(int health) {
        fill.maxValue = health;
        fill.value = health;
        fillColor.color = gradient.Evaluate(1f);
    }

    public void setHealth(int health) {
        fill.value = health;
        fillColor.color = gradient.Evaluate(fill.normalizedValue);
    }

}
