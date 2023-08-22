using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private float initialMovementSpeed = 20f;
    [SerializeField] private float maxMovementSpeed = 30f;
    private PlayerHandler playerHandler;
    private Vector2 inputVector;

    private float accelarationTimer = 0f;

    private void Awake() {
        playerHandler = GetComponent<PlayerHandler>();
    }

    private void Start() {
        movementSpeed = initialMovementSpeed;
        playerHandler.OnColision += PlayerHandler_OnColision;
    }

    private void PlayerHandler_OnColision(object sender, System.EventArgs e) {
        transform.position = transform.position + new Vector3(0.01f, 0);
    }

    private void Update() {
        inputVector = GameInput.Instance.GetMovementNormalized();
        MovePlayer();
        MovementAcceleration();
    }


    private void MovePlayer() {
        transform.position += (Vector3)inputVector * (movementSpeed * Time.deltaTime);
    }

    private void MovementAcceleration() {
        if (inputVector.x == 0f) {
            accelarationTimer = 0f;
            ResetMovementSpeed();
            return;
        }
        accelarationTimer += Time.deltaTime;
        if (accelarationTimer <= 0.5f) {
            return;
        }
        if (movementSpeed < maxMovementSpeed) {
            movementSpeed += 2f;
            accelarationTimer = 0f;
        }
    }

    
    private void ResetMovementSpeed() {
        movementSpeed = initialMovementSpeed;
    }
}
