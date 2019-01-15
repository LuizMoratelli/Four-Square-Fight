using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;

    private int currentHealth;
    private int maxHealth;

    public HealthSystem(int initialHealth)
    {
        currentHealth = initialHealth;
        maxHealth = initialHealth;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public float GetHealthPercent()
    {
        // Necessário converter para que o valor retornado seja fração
        return (float)currentHealth / maxHealth;
    }

    public void Damage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        //if (OnHealthChanged != null)
        //{
        //    OnHealthChanged(this, EventArgs.Empty);
        //}

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void FullHeal()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
}
