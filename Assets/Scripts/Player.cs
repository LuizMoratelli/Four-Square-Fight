using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Range(1.0f, 100.0f)]
    public float speed;

    private int _health;
    public int Health { 
        get {
            return _health;
        }
        set {
            if (value <= hearts.Length)
            {
                _health = value;
            }

            UpdateHealthUI();

            if (_health <= 0)
            {
                Die();
            }
        }
    }

    public Transform[] weaponSlots;

    private Rigidbody2D _rb;
    private Vector2 _moveAmount;

    [Header("Effects")]
    public GameObject dieEffect;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Animator hurtAnimator;

    private SceneTransitions sceneTransitions;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Health = hearts.Length;
        sceneTransitions = FindObjectOfType<SceneTransitions>();
    }

    private void Update()
    {
        Vector2 movementInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        // Previne o movimento Horizontal e Vertical simultaneamente (dobrando a velocidade)
        _moveAmount = movementInput.normalized * speed;
    }

    internal void Heal(int healAmount)
    {
        Health += healAmount;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveAmount * Time.fixedDeltaTime);
    }

    public void Damage(int amount)
    {
        Health -= amount;
        hurtAnimator.SetTrigger("hurt");
    } 

    public void Die()
    {
        Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        sceneTransitions.LoadScene("Lose");
    }

    public void ChangeWeapon(Weapon weaponToEquip, bool doubleWeapon)
    {
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");

        foreach (var weapon in weapons)
        {
            Destroy(weapon);
        }

        Instantiate(weaponToEquip, weaponSlots[0].position, weaponSlots[0].rotation, weaponSlots[0]);

        if (doubleWeapon)
        {
            Instantiate(weaponToEquip, weaponSlots[1].position, weaponSlots[1].rotation, weaponSlots[1]);
        }
    }

    private void UpdateHealthUI()
    {
        int currentHealth = Health;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            } else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
