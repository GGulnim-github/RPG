using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public Vector2 move;
    public Vector3 moveDirection;

    public bool dash;
    public bool jump;
    public bool isWalk;
    public bool attack;

    PlayerController _controller;

    private void Start()
    {
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
        Cursor.lockState = CursorLockMode.Locked;

        _controller = GetComponent<PlayerController>();
    }

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnJump(InputValue value)
    {
        if (_controller.isGrounded == false 
            || _controller.isAttack == true)
        {
            return;
        }
        jump = value.isPressed;
    }

    public void OnDash(InputValue value)
    {
        dash = value.isPressed;
    }

    public void OnAttack(InputValue value)
    {
        if (_controller.isGrounded == false)
        {
            return;
        }

        attack = value.isPressed;
    }

    public void OnChangeWalkRun(InputValue value)
    {
        isWalk = !isWalk;
    }

    public void MoveInput(Vector2 value)
    {
        move = value;
        moveDirection = new Vector3(move.x, 0.0f, move.y).normalized;
    }
}
