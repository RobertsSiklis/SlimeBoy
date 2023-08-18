using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float movementSpeed = 0.2f;
    [SerializeField] private float savedMoveSpeed = 0.2f;
    [SerializeField] private float jumpThrust = 20f;
    [SerializeField] private float savedJumpThrust = 20f;
    [SerializeField] private float fallSpeed = 20f;
    [SerializeField] private float savedFallSpeed = 20f;
    
    private float time = 0f;

    private bool isRunning;
    private bool isGrounded;
    private bool isJumping;
    private Vector2 inputVector;

    private void Update() {
        inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.A)) {
            inputVector = new Vector2(-1, 0);
            isRunning = true;
        } else if (Input.GetKey(KeyCode.D)) {
            inputVector = new Vector2(1, 0);
            isRunning = true;
        } else {
            isRunning = false;
        }

        isGrounded = CastGroundCheckRay();
        if (!isGrounded && !isJumping) {
            transform.position += Vector3.down * (fallSpeed * Time.deltaTime);
            if (fallSpeed < 300) {
                fallSpeed++;
            }
        }
        if (isGrounded) {
            fallSpeed = savedFallSpeed;
        }
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            isJumping = true;
        }
        if (isJumping) {
            transform.position += Vector3.up * (jumpThrust * Time.deltaTime);
            jumpThrust--;
        }

        if (jumpThrust < 2) {
            isJumping = false;
            jumpThrust = savedJumpThrust;
        }
        MovePlayer();
        if (isRunning) {
            time += Time.deltaTime;
            if (time > 0.2f) {
                if(movementSpeed < 30f) {
                    movementSpeed += 2f;
                }
                time = 0;
            }
        }
        else {
            time = 0;
        }

        if (!isRunning) {
            movementSpeed = savedMoveSpeed;
        }
    }

    private bool CastGroundCheckRay() {
        RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(1f, 1f), 0, Vector2.down, 0.05f, groundLayerMask);
        return ray.collider != null;
    }

    private void MovePlayer() {
        transform.position += (Vector3)inputVector * (movementSpeed * Time.deltaTime);
    }
    private void Jump() {
       // rb.velocity = new Vector2(0, 3f) * (jumpThrust + Time.fixedDeltaTime);
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, -0.2f), new Vector2(1f, 1f));
    }
}
