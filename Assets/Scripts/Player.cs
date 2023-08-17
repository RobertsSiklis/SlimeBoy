using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float jumpThrust = 20f;
    private bool canJump;
    private bool isMoving;
    private Vector2 inputVector;
    private Rigidbody2D rb;
    
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update() {
        inputVector = new Vector2(0, 0);
        isMoving = false;
        canJump = false;
        if (Input.GetKey(KeyCode.A)) {
            inputVector = new Vector2(-1, 0);
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.D)) {
            inputVector = new Vector2(1, 0);
            isMoving = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && CastGroundCheckRay()) {
            Jump();
        }

    }

    private bool CastGroundCheckRay() {
        RaycastHit2D ray = Physics2D.Raycast(transform.position + new Vector3(0, -0.6f, 0), Vector2.down, 0.1f, groundLayerMask);
        return ray.collider != null;
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void MovePlayer() {
        rb.MovePosition(rb.position + movementSpeed * Time.fixedDeltaTime * inputVector);
    }
    private void Jump() {
        rb.velocity = Vector2.up * jumpThrust;
    }


    private void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position + new Vector3(0, -0.6f, 0), new Vector3(0, -1.1f, 0));
    }
}
