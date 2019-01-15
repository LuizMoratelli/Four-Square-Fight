using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform[] shotPoints;
    public float timeBtwShots;

    private float shotTime;
    private Animator cameraAnimator;
    //private bool isRight;

    void Start()
    {
        //isRight = true;
        cameraAnimator = Camera.main.GetComponent<Animator>();
    }

    void Update()
    {
        // Distância da posição do Mouse e da Arma
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //if (((angle > 90f && angle < 180f) || (angle < -90f && angle > -180f)) && isRight)
        //{
        //    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        //    isRight = false;
        //}
        //else if (((angle < 90f && angle > 0f) || (angle < 0 && angle > -90f)) && !isRight)
        //{
        //    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        //    isRight = true;
        //}

        //float deltaRotation = 0f;
        //if (!isRight)
        //{
        //    deltaRotation = 180f;
        //}

        Quaternion rotation = Quaternion.AngleAxis(angle /*- deltaRotation*/, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time >= shotTime)
            {
                cameraAnimator.SetTrigger("shake");
                foreach (var shotPoint in shotPoints)
                {
                    Instantiate(projectile, shotPoint.position, transform.rotation);
                }
                
                shotTime = Time.time + timeBtwShots;
            }
        }
    }
}
