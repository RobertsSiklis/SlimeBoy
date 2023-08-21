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
    private PlayerJump playerJump;

    private bool canFall = true;

    private void Awake() {
        playerHandler = GetComponent<PlayerHandler>();
        playerJump = GetComponent<PlayerJump>();
    }
    private void Start() {
        fallSpeed = initalFallSpeed;
        playerHandler.OnNotGrounded += PlayerHandler_OnNotGrounded;
        playerHandler.OnGrounded += PlayerHandler_OnGrounded;
        playerJump.OnJumping += PlayerJump_OnJumping;
        playerJump.OnJumpingFinished += PlayerJump_OnJumpingFinished;
    }

    private void PlayerHandler_OnNotGrounded(object sender, System.EventArgs e) {
        if (canFall) {
            Fall();
        }
    }

    private void PlayerHandler_OnGrounded(object sender, System.EventArgs e) {
        ResetToInitialFallSpeed();
    }

    private void PlayerJump_OnJumping(object sender, System.EventArgs e) {
        canFall = false;
    }

    private void PlayerJump_OnJumpingFinished(object sender, System.EventArgs e) {
        canFall = true;
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
