using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private float currentHealth;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        currentHealth = health;
        SetHealth();
    }

    public void SetHealth()
    {
        slider.value = currentHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        SetHealth();
    }
}
