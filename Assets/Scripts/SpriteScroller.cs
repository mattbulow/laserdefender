using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] Vector2 baseMoveSpeed;
    [SerializeField] float inputSpeedMultiplier = 0.5f;
    [SerializeField] Player player;

    Vector2 moveInput = Vector2.zero;
    Vector2 offset;
    Material material;
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            moveInput = player.GetMoveInput();
        }
        //Debug.Log("MoveInput = " + moveInput);
        offset.x = (baseMoveSpeed.x * moveInput.x) * Time.deltaTime;
        offset.y = (baseMoveSpeed.y + baseMoveSpeed.y*moveInput.y* inputSpeedMultiplier) * Time.deltaTime;
        material.mainTextureOffset += offset;
    }

}
