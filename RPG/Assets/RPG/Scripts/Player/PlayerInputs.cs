using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInputs : MonoBehaviour
{
    public Vector2 move;
    public Vector3 moveDirection;
    public Vector2 look;
    public float zoom;
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

    public void OnLook(InputValue value)
    {
        LookInput(value.Get<Vector2>());
    }

    public void OnZoom(InputValue value)
    {
        ZoomInput(value.Get<float>());
    }

    public void MoveInput(Vector2 value)
    {
        move = value;
        moveDirection = new Vector3(move.x, 0.0f, move.y).normalized;
    }

    public void LookInput(Vector2 value)
    {
        look = value;
    }

    public void ZoomInput(float value)
    {
        zoom = value;
    }
}
