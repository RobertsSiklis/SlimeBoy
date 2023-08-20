using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float fallSpeed;

    [SerializeField] private float initialMovementSpeed = 20f; 
    [SerializeField] private float initialJumpVelocity = 20f;
    [SerializeField] private float initalFallSpeed = 20f;

    [SerializeField] private float maxFallSpeed = 300f;
    [SerializeField] private float maxMovementSpeed = 30f;


    private float time = 0f;

    private bool isGrounded;
    private bool isJumping;
    private Vector2 inputVector;

    private void Start() {
        movementSpeed = initialMovementSpeed;
        jumpVelocity = initialJumpVelocity;
        fallSpeed = initalFallSpeed;
        
    }

    private void Update() {

        inputVector = GameInput.Instance.GetMovementNormalized();
        isGrounded = GetCastGroundCheckRay();

        MovePlayer();
        MovementAcceleration();

        Jump();
        ResetJumpVelocity();

        Fall();
        IncreaseFallSpeed();
        ResetToInitialFallSpeed();
    }

    private bool GetCastGroundCheckRay() {
        RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(1f, 1f), 0, Vector2.down, 0.05f, groundLayerMask);
        return ray.collider != null;
    }

    private void MovePlayer() {
        transform.position += (Vector3)inputVector * (movementSpeed * Time.deltaTime);
    }

    private void MovementAcceleration() {
        if (inputVector.x != 0f) {
            time += Time.deltaTime;
            if (time > 0.5f) {
                if (movementSpeed < maxMovementSpeed) {
                    movementSpeed += 2f;
                    time = 0f;
                }
            }
        } else {
            time = 0f;
            ResetMovementSpeed();
        }
    }

    private void ResetMovementSpeed() {
        movementSpeed = initialMovementSpeed;
    }

    private void Jump() {
        if (isGrounded && GameInput.Instance.JumpPressed()) {
            isJumping = true;
        }
        if (isJumping) {
            transform.position += Vector3.up * (jumpVelocity * Time.deltaTime);
            DecreaseJumpVelocity();
        }
    }

    private void DecreaseJumpVelocity() {
        jumpVelocity--;
    }

    private void ResetJumpVelocity() {
        if (jumpVelocity < 2) {
            isJumping = false;
            jumpVelocity = initialJumpVelocity;
        }
    }

    private void Fall() {
        if (!isGrounded && !isJumping) {
            transform.position += Vector3.down * (fallSpeed * Time.deltaTime);
        }
    }

    private void IncreaseFallSpeed() {
        if (fallSpeed < maxFallSpeed) {
            fallSpeed++;
        }
    }

    private void ResetToInitialFallSpeed() {
        if (isGrounded) {
            fallSpeed = initalFallSpeed;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, -0.2f), new Vector2(1f, 1f));
    }
}
