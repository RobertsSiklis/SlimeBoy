using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public event EventHandler OnJumping;
    public event EventHandler OnJumpingFinished;

    [SerializeField] private float initialJumpVelocity = 20f;
    [SerializeField] private float jumpVelocity;

    private PlayerHandler playerHandler;
    private bool isGrounded;
    private bool isJumping;

    private void Awake() {
        playerHandler = GetComponent<PlayerHandler>();
    }

    void Start()
    {
        jumpVelocity = initialJumpVelocity;
        GameInput.Instance.OnJumpPressed += GameInput_OnJumpPressed;
        playerHandler.OnGrounded += PlayerHandler_OnGrounded;
        playerHandler.OnRoofColision += PlayerHandler_OnRoofColision;

    }

    private void PlayerHandler_OnRoofColision(object sender, EventArgs e) {
        isJumping = false;
        jumpVelocity = initialJumpVelocity;
        OnJumpingFinished?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerHandler_OnGrounded(object sender, System.EventArgs e) {
        isGrounded = true;
    }

    private void GameInput_OnJumpPressed(object sender, System.EventArgs e) {
        if (isGrounded) {
            isJumping = true;            
        }
        isGrounded = false;
    }

    void Update()
    {
        if (isJumping) {
            OnJumping?.Invoke(this, EventArgs.Empty);
            Jump();
            DecreaseJumpVelocity();
            ResetJumpVelocity();
        }
        Debug.Log("Player can Jump" + isJumping);
    }

    private void Jump() {
        transform.position += Vector3.up * (jumpVelocity * Time.deltaTime);
    }

    private void DecreaseJumpVelocity() {
        jumpVelocity--;
    }

    private void ResetJumpVelocity() {
        if (jumpVelocity < 2) {
            isJumping = false;
            jumpVelocity = initialJumpVelocity;
            OnJumpingFinished?.Invoke(this, EventArgs.Empty);
        }
    }
}
