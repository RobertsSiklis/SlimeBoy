using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float movementSpeed = 10f;
    private bool isGrounded;
    private bool isJumping;
    private Vector2 inputVector;
    private Rigidbody2D rb;
    

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        CastGroundCheckRay();
        inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.A)) {
            inputVector = new Vector2(-1, 0);
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector = new Vector2(1, 0);
        }
        if (Input.GetKey(KeyCode.Space)) {
            isJumping = true;
        }
        isJumping = false;
        
    }

    private void CastGroundCheckRay() {
        RaycastHit2D ray = Physics2D.Raycast(transform.position + new Vector3(0, -0.6f, 0), Vector2.down, 1f, groundLayerMask);
        if (ray.collider != null) {
            isGrounded = true;
        }
        isGrounded = false;
    }


    private void FixedUpdate() {
        MovePlayer();
        Debug.Log(isGrounded + " " + isJumping);
        if (isGrounded && isJumping) {
            Jump();
        }
    }

    private void MovePlayer() {
        rb.MovePosition(rb.position + movementSpeed * Time.fixedDeltaTime * inputVector);
    }
    private void Jump() {
        rb.AddForce(transform.up * 2f, ForceMode2D.Impulse);
    }


    private void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position + new Vector3(0, -0.6f, 0), new Vector3(0, -2f, 0));
    }
}
