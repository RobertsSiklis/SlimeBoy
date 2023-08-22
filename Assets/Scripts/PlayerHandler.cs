using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHandler : MonoBehaviour
{
    public event EventHandler OnNotGrounded;
    public event EventHandler OnGrounded;
    public event EventHandler OnRoofColision;
    public event EventHandler OnColision;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform groundColliderRight;
    [SerializeField] private Transform groundColliderLeft;
    [SerializeField] private Transform roofCollider;
    [SerializeField] private Transform leftCollider;
    [SerializeField] private Transform rightCollider;

    private void Update() {
        //GetBoxCastRoofCheckRay();
        GetBoxCastGroundCheckRay();
        //GetBoxCastLeftCheckRay();
        //GetBoxCastRightCheckRay();
    }

    //private void GetBoxCastGroundCheckRay() {
    //    Vector2 rayOrigin = groundCollider.transform.position;
    //    Vector2 rayColliderSize = new Vector2(1, 0.1f);
    //    float rayAngle = 0;
    //    Vector2 rayDirection = Vector2.down;
    //    float rayDistance = 0f;
    //    if (!Physics2D.BoxCast(rayOrigin, rayColliderSize, rayAngle, rayDirection, rayDistance, groundLayerMask)) {
    //        OnNotGrounded?.Invoke(this, EventArgs.Empty);
    //    }
    //    else {
    //        OnGrounded?.Invoke(this, EventArgs.Empty);
    //    }
    //}

    private void GetBoxCastGroundCheckRay() {
        Vector2 rayOrigin = groundColliderLeft.transform.position;
        Vector2 rayOrigin2 = groundColliderRight.transform.position;
        Vector2 rayColliderSize = new Vector2(1, 0.1f);
        Vector2 rayDirection = Vector2.down;
        float rayDistance = 0.2f;
        if (!Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, groundLayerMask) && !Physics2D.Raycast(rayOrigin2, rayDirection, rayDistance, groundLayerMask)) {
            OnNotGrounded?.Invoke(this, EventArgs.Empty);
        }
        else {
            OnGrounded?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GetBoxCastRoofCheckRay() {
        Vector2 rayOrigin = roofCollider.transform.position;
        Vector2 rayColliderSize = new Vector2(1, 0.1f);
        float rayAngle = 0;
        Vector2 rayDirection = Vector2.up;
        float rayDistance = 0f;

        if (Physics2D.BoxCast(rayOrigin, rayColliderSize, rayAngle, rayDirection, rayDistance, groundLayerMask)) {
            Debug.Log("Hit");
            OnRoofColision?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GetBoxCastLeftCheckRay() {

        Vector2 rayOrigin = leftCollider.transform.position;
        Vector2 rayColliderSize = new Vector2(0.1f, 0.8f);
        float rayAngle = 0;
        Vector2 rayDirection = Vector2.left;
        float rayDistance = 0f;

        if (Physics2D.BoxCast(rayOrigin, rayColliderSize, rayAngle, rayDirection, rayDistance, groundLayerMask)) {
            Debug.Log("Hit");
            OnColision?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GetBoxCastRightCheckRay() {

        Vector2 rayOrigin = rightCollider.transform.position;
        Vector2 rayColliderSize = new Vector2(0.1f, 1f);
        float rayAngle = 0;
        Vector2 rayDirection = Vector2.right;
        float rayDistance = 0f;

        if (Physics2D.BoxCast(rayOrigin, rayColliderSize, rayAngle, rayDirection, rayDistance, groundLayerMask)) {
            Debug.Log("Hit");
            OnColision?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(groundColliderRight.transform.position, groundColliderRight.transform.position + new Vector3(0, -0.2f));
        Gizmos.DrawLine(groundColliderLeft.transform.position, groundColliderLeft.transform.position + new Vector3(0, -0.2f));
        //Gizmos.DrawWireCube(groundCollider.transform.position, new Vector2(1f, 0.1f));
        //Gizmos.DrawWireCube(roofCollider.transform.position , new Vector2(1f, 0.1f));
        //Gizmos.DrawWireCube(leftCollider.transform.position, new Vector2(0.1f, 0.8f));
        //Gizmos.DrawWireCube(rightCollider.transform.position, new Vector2(0.1f, 1f));
    }
}
