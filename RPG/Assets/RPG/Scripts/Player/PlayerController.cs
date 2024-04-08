using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerInputs Inputs { get; private set; }

    public Animator Animator { get; private set; }
    public CharacterController CharacterController { get; private set; }

    public Vector3 AnimatorDeltaPosition { get; private set; }

    public float rotationSmoothTime = 0.12f;
    [HideInInspector] public float targetRotation = 0.0f;
    [HideInInspector] public float rotationVelocity;
    [HideInInspector] public float verticalVelocity;

    public Transform cameraTransform;

    private void Awake()
    {
        Inputs = GetComponent<PlayerInputs>();

        Animator = GetComponent<Animator>();
        CharacterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        StateMachine = new PlayerStateMachine(this);
    }

    private void Update()
    {
        StateMachine.Update();
    }

    private void OnAnimatorMove()
    {
        AnimatorDeltaPosition = Animator.deltaPosition;
    }
}
