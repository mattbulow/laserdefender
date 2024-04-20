using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float boundsPaddingLeftRight = 0.5f;
    [SerializeField] float boundsPaddingTop = 0.5f;
    [SerializeField] float boundsPaddingBottom = 2f;

    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;

    PlayerShooter shooter;

    private void Awake()
    {
        shooter = GetComponent<PlayerShooter>();
    }

    void Start()

    {
        InitBounds();
    }
    void Update()
    {
        Move();
    }
    
    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + boundsPaddingLeftRight, maxBounds.x - boundsPaddingLeftRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + boundsPaddingBottom, maxBounds.y - boundsPaddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        //Debug.Log("OnMove rawInput = " + rawInput);
    }
    public Vector2 GetMoveInput() { return rawInput; }

    void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.SetIsFiring(value.isPressed);
        }

    }

}
