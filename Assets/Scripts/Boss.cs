using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
    public Enemy[] enemies;
    public float spawnOffset;

    private int halfHealth;
    private Animator animator;

    private Slider healthBar;
    private SceneTransitions sceneTransitions;

    public override void Start()
    {
        base.Start();
        
        halfHealth = health / 2;
        animator = GetComponent<Animator>();
        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
        sceneTransitions = FindObjectOfType<SceneTransitions>();
    }

    public override void Damage(int amount)
    {
        base.Damage(amount);

        healthBar.value = health;

        if (health <= halfHealth)
        {
            animator.SetTrigger("Stage2");
        } else
        {
            Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
            Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Damage(damage);
        }
    }

    public override void Die()
    {
        base.Die();

        healthBar.gameObject.SetActive(false);
        sceneTransitions.LoadScene("Win");
    }
}
