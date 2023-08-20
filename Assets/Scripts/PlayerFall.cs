using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    [SerializeField] private float initalFallSpeed = 20f;
    [SerializeField] private float maxFallSpeed = 300f;
    [SerializeField] private float fallSpeed;

    private PlayerHandler playerHandler;

    private void Awake() {
        playerHandler = GetComponent<PlayerHandler>();
    }
    private void Start() {
        fallSpeed = initalFallSpeed;
        playerHandler.OnNotGrounded += PlayerHandler_OnNotGrounded;
        playerHandler.OnGrounded += PlayerHandler_OnGrounded;
    }

    private void PlayerHandler_OnNotGrounded(object sender, System.EventArgs e) {
        Fall();
    }

    private void PlayerHandler_OnGrounded(object sender, System.EventArgs e) {
        ResetToInitialFallSpeed();
    }

    private void Fall() {
        transform.position += Vector3.down * (fallSpeed * Time.deltaTime);
        IncreaseFallSpeed();
    }

    private void IncreaseFallSpeed() {
        if (fallSpeed < maxFallSpeed) {
           fallSpeed++;
        }
    }

    private void ResetToInitialFallSpeed() {
        fallSpeed = initalFallSpeed;
    }
}
