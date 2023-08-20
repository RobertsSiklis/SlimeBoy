using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public Vector2 GetMovementNormalized() {

        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x = 1;
        }

        inputVector = inputVector.normalized;
        return inputVector;
    }

    public bool JumpPressed() {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
