using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
    Vector2 speed = Vector2.zero;

    public void SetSpeed(Vector2 newSpeed) { speed = newSpeed; }

    void Update()
    {
        transform.Translate(speed*Time.deltaTime);
    }
}
