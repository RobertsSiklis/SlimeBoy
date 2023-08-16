using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    private Vector2 inputVector;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {

        inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.A)) {
            inputVector = new Vector2(-1, 0);
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector = new Vector2(1, 0);
        }

    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + movementSpeed * Time.fixedDeltaTime * inputVector);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position, new Vector3(0, -3f));
    }
}
