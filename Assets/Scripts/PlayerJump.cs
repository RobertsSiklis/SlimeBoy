using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float initialJumpVelocity = 20f;


    [SerializeField] private float jumpVelocity;
    private bool isJumping;
    // Start is called before the first frame update
    void Start()
    {
        jumpVelocity = initialJumpVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        // Jump();
        // ResetJumpVelocity();
    }

    private void Jump() {
        //if (isGrounded && GameInput.Instance.JumpPressed()) {
        //    isJumping = true;
        //}
        //if (isJumping) {
        //    transform.position += Vector3.up * (jumpVelocity * Time.deltaTime);
        //    DecreaseJumpVelocity();
        //}
    }

    //private void DecreaseJumpVelocity() {
    //    jumpVelocity--;
    //}

    //private void ResetJumpVelocity() {
    //    if (jumpVelocity < 2) {
    //        isJumping = false;
    //        jumpVelocity = initialJumpVelocity;
    //    }
    //}
}
