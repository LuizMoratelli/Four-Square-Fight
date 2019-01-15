using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform toFollow;
    [Range(0f, 10f)]
    public float speed = 1;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void Start()
    {
        transform.position = toFollow.position;
    }

    void Update()
    {
        if (toFollow != null)
        {
            // Limita o valor máximo e mínimo
            float clampedX = Mathf.Clamp(toFollow.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(toFollow.position.y, minY, maxY);

            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), speed);
        }
    }
}
