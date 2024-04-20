using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class ProjectileEnemy2 : MonoBehaviour
{
    [SerializeField] int rotateRate = 180;
    [SerializeField] float flashRate = 1f;
    [SerializeField] float scaleMin = 0.5f;
    [SerializeField] float scaleMax = 0.7f;

    float speed = 4f;
    Vector2 scale;
    bool increaseScale = true;
    float rotateDirection;
    Vector2 targetPosition;

    Transform player;

    public void SetSpeed(float newSpeed) { speed = newSpeed; }

    private void Awake()
    {
        scale = new Vector2(scaleMin, scaleMin);
        rotateDirection = Mathf.Sign(Random.Range(-1, 1));
        player = FindAnyObjectByType<Player>().transform;
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
        Flash();
        Rotate();
        Move();
    }
    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
    void Rotate()
    {
        transform.Rotate(0, 0, rotateRate * Time.deltaTime* rotateDirection);
    }

    void Flash()
    {
        
        if (transform.localScale.x<= scaleMin)
        {
            increaseScale = true;
        }
        else if (transform.localScale.x>= scaleMax)
        {
            increaseScale = false;
        }
        if (increaseScale)
        {
            scale.x += flashRate * Time.deltaTime;
            scale.y = scale.x;
        }
        else
        {
            scale.x -= flashRate * Time.deltaTime;
            scale.y = scale.x;
        }
        transform.localScale = scale;


    }
}
