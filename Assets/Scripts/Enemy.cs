using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public int damage;
    public float timeBtwAttacks;
    public float attackSpeed;
    public float stopDistance;
    public int pickupChance;
    public Pickup[] pickups;

    protected float attackTime;
    protected static Transform player;

    [Header("Effects")]
    public GameObject dieEffect;
    
    public virtual void Damage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }
    
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void Die()
    {
        int randomNumber = Random.Range(0, 101);

        if (pickupChance >= randomNumber)
        {
            int randNum = Random.Range(0, pickups.Length);
            Pickup randomPickup = pickups[randNum];
            Instantiate(randomPickup, transform.position, transform.rotation);
        }

        Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
