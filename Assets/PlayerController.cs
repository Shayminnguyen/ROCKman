using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(CustomCharacterController))]
public class PlayerController : MonoBehaviour {
    public float runSpeed;
    public float jumpSpeed;
    public float wallSliding;
    public float dashSpeed;
    public int jumpCount;
    public int dashCount;

    private CustomCharacterController characterController;

    private Collider2D coll;
    private Vector2 currentVelocity;
    private int currentDirection;

    private void Awake() {
        characterController = GetComponent<CustomCharacterController>();
        coll = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (characterController.collisionInfo.collideRight || characterController.collisionInfo.collideLeft)
        {
            currentVelocity.y = -wallSliding;
            jumpCount = 1;
        }
        else
        {
            currentVelocity += Physics2D.gravity * Time.fixedDeltaTime;
        }
        Movement();
        characterController.Move(currentVelocity);
        if (characterController.collisionInfo.collideBottom && currentVelocity.y <= 0)
        {
            currentVelocity.y = 0;
            jumpCount = 0;
            dashCount = 0;
        }
        //if(Mathf.Abs(currentVelocity.x) != dashSpeed)
        currentVelocity.x = 0;
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentVelocity.x = -runSpeed;
            //transform.position += (Vector3)currentVelocity;
            currentDirection = -1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentVelocity.x = runSpeed;
            //transform.position += (Vector3)currentVelocity;
            currentDirection = 1;
        }

        if (Input.GetKey(KeyCode.Space) && currentVelocity.y < 2*jumpSpeed/3 && jumpCount < 2)
        {
            if (jumpCount == 1 && dashCount > 0) { }
            else
            {
                currentVelocity.y = jumpSpeed;
                jumpCount++;
            }
            //transform.position += (Vector3)currentVelocity;
        }

        if (Input.GetKey(KeyCode.C) && jumpCount == 1 && dashCount < 10)
        {
            currentVelocity.x = dashSpeed * currentDirection;
            //jumpCount++;
            dashCount++;
        }
    }
}
