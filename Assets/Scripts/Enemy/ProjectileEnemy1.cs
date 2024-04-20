using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy1 : MonoBehaviour
{
    float speed = 5f;

    public void SetSpeed(float newSpeed) { speed = newSpeed; }

    void Update()
    {
        Move();
    }
    void Move()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
    }
}
