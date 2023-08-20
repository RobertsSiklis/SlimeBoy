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
    [SerializeField] private LayerMask groundLayerMask;

    private void Update() {
        GetCastGroundCheckRay();
    }

    public void GetCastGroundCheckRay() {
        float rayAngle = 0;
        Vector2 rayColliderSize = new Vector2(1, 1);
        Vector2 rayDirection = Vector2.down;
        float rayDistance = 0.05f;

        if (!Physics2D.BoxCast(transform.position, rayColliderSize, rayAngle, rayDirection, rayDistance, groundLayerMask)) {
            OnNotGrounded?.Invoke(this, EventArgs.Empty);
        } else {
            OnGrounded?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, -0.2f), new Vector2(1f, 1f));
    }
}
