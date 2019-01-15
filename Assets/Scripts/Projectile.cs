using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Gereral Variables")]
    public float speed;
    public float lifeTime;
    public int damage;

    [Header("Effects")]
    public GameObject onDestroyEffect;

    [Header("Decrease Bullet Scale w/ Time")]
    public bool isDecreased;
    public float maxSize;
    public float minSize;
    public float sizeDecrease;
    public float timeToDecrease;

    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        Invoke("DestroyProjectile", lifeTime);

        if (isDecreased)
        {
            StartCoroutine(Decrease());
            transform.localScale = new Vector2(maxSize, maxSize);
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void DestroyProjectile()
    {
        Instantiate(onDestroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().Damage(damage);
            DestroyProjectile();
        }
    }

    IEnumerator Decrease()
    {
        yield return new WaitForSeconds(timeToDecrease);
        if (transform.localScale.x > minSize)
        {
            Vector2 newSize = new Vector2(transform.localScale.x - sizeDecrease, transform.localScale.y - sizeDecrease);
            transform.localScale = newSize;
            StartCoroutine(Decrease());
        }
    }
}
