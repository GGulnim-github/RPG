using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInputs : MonoBehaviour
{
    public Vector2 move;
    public Vector3 moveDirection;

    public bool jump;
    public bool isWalk;

    private void Start()
    {
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
    }

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void MoveInput(Vector2 value)
    {
        move = value;
        moveDirection = new Vector3(move.x, 0.0f, move.y).normalized;
    }
}
