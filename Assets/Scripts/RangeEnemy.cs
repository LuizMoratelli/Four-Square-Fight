using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    public Transform shotPoint;
    public GameObject bullet;
    public float maxDistance;

    void Update()
    {
        if (player != null)
        {
            float currentDistance = Vector2.Distance(transform.position, player.position);

            if (currentDistance > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

            if (currentDistance <= maxDistance)
            {
                if (Time.time >= attackTime)
                {
                    attackTime = Time.time + timeBtwAttacks;
                    RangedAttack();
                }
            }
        }
    }

    public void RangedAttack()
    {
        Vector2 direction = player.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        shotPoint.rotation = rotation;

        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
    }
}
