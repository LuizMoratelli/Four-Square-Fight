using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Player playerScript;
    private Vector2 targetPosition;

    public float lifeTime;
    public float speed;
    public int damage;
    public GameObject onDestroyEffect;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = playerScript.transform.position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, targetPosition) > .1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        } else
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerScript.Damage(damage);
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Instantiate(onDestroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
