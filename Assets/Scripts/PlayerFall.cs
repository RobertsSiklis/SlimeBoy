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

    private bool canFall;
    private float timer = 0f;
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
    private void Update() {
        Debug.Log("Player can fall" + canFall);
        if (canFall) {
            Fall();
        }
    }
    private void PlayerHandler_OnNotGrounded(object sender, System.EventArgs e) {
        canFall = true;
    }

    private void PlayerJump_OnJumpingFinished(object sender, System.EventArgs e) {
        canFall = true;
    }

    private void PlayerHandler_OnGrounded(object sender, System.EventArgs e) {
        canFall = false;
        ResetToInitialFallSpeed();  
    }

    private void PlayerJump_OnJumping(object sender, System.EventArgs e) {
        canFall = false;
    }


    private void Fall() {
        transform.position += Vector3.down * (fallSpeed * Time.deltaTime);
        IncreaseFallSpeed();
    }

    private void IncreaseFallSpeed() {
        
        timer += Time.deltaTime;
        if (timer >= 0.1f) {
            if (fallSpeed < maxFallSpeed) {
                fallSpeed++;
            }
            timer = 0f;
        }
        Debug.Log(timer);

    }

    private void ResetToInitialFallSpeed() {
        fallSpeed = initalFallSpeed;
    }
}
