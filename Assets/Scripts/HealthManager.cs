using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int initialHealth;
    public GameObject lifeBar; 
    
    private Transform _lifeBar;
    private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = new HealthSystem(initialHealth);
        _lifeBar = lifeBar.transform;
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, EventArgs e)
    {
        _lifeBar.localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    }

    private void Update()
    {
        //_lifeBar.localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            healthSystem.Damage(10);
        }
    }
}
