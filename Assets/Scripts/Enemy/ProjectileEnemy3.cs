using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class ProjectileEnemy3 : MonoBehaviour
{
    [SerializeField] int rotateRate = 120;

    float speed = 1f;
    Vector2 targetPosition;

    Transform player;

    public void SetSpeed(float newSpeed) { speed = newSpeed; }

    private void Awake()
    {
        player = FindAnyObjectByType<Player>(FindObjectsInactive.Include).transform;
    }

    private void Start()
    {
        // want to move projectile towards location player was when this object was created
        targetPosition = player.transform.position;
        Vector2 currentPosition = transform.position;
        // need to get a point past the player
        // first get vector pointing to target from this object
        Vector2 playerDirection = targetPosition - currentPosition;
        //scale playerDirection vector by arbitrary large amount and add to target to get new target
        targetPosition = 100 * playerDirection + targetPosition;
    }

    void Update()
    {
        Rotate();
        Move();
    }
    void Move()
    {
        // Only move in the direction the missile is currently facing. Sprite is set up so missile 'foward' is the -y direction.
        transform.Translate(0, -speed * Time.deltaTime, 0, Space.Self);
    }
    void Rotate()
    {
        // Get the direction from this object to the target object
        Vector3 direction = player.position - transform.position;
        // Calculate the angle between this object  and target, in degrees
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        // Rotate towards the target angle at constant (max) rotation rate.
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotateRate * Time.deltaTime);
        // Apply the rotation around the Z-axis
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
