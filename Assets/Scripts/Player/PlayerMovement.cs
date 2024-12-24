using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    public float moveSpeed;
    Rigidbody2D rb;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 lastMovedVector;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    void FixedUpdate ()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f); // last moved x

        }

        if (moveDir.y != 0)
        {
            lastHorizontalVector = moveDir.y;
        }

        if (moveDir.y != 0)
        {
            lastHorizontalVector = moveDir.y;
            lastMovedVector = new Vector2 (0f , lastVerticalVector); // last moved y  
        }
        else if (moveDir.x != 0 && moveDir.y != 0)
        {
            //lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector); // while moving
        } 

    }
    void Move()
    {
        rb.linearVelocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}
