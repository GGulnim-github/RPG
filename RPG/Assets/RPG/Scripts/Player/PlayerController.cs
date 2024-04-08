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

    [Header("Camera Setting")]
    public CinemachineVirtualCamera virtualCamera;
    public Transform cameraTarget;
    [SerializeField] float cameraTopClamp = 70.0f;
    [SerializeField] float cameraBottomClamp = -30.0f;
    [SerializeField] float cameraXAxisSpeed = 0.1f;
    [SerializeField] float cameraYAxisSpeed = 0.1f;
    [SerializeField] float cameraFarDistance = 5f;
    [SerializeField] float cameraNearDistance = 1f;
    [SerializeField] float cameraZoomSmooth = 3f;
    Cinemachine3rdPersonFollow _3rdPersonFollow;
    float _currentCameraTargetDistance;
    Quaternion _lastLookRotation;

    private void Awake()
    {
        Inputs = GetComponent<PlayerInputs>();

        Animator = GetComponent<Animator>();
        CharacterController = GetComponent<CharacterController>();

        _3rdPersonFollow = virtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        _currentCameraTargetDistance = _3rdPersonFollow.CameraDistance;
    }

    private void Start()
    {
        StateMachine = new PlayerStateMachine(this);
    }

    private void Update()
    {
        CameraZoom();
        StateMachine.Update();
    }

    private void LateUpdate()
    {
        CameraRotate();
    }

    private void OnAnimatorMove()
    {
        AnimatorDeltaPosition = Animator.deltaPosition;
    }

    void CameraRotate()
    {
        Vector3 targetEulerAngles = _lastLookRotation.eulerAngles;
        if (Inputs.look.sqrMagnitude >= 0.01f)
        {
            targetEulerAngles.x -= Inputs.look.y * cameraXAxisSpeed;
            targetEulerAngles.y += Inputs.look.x * cameraYAxisSpeed;
        }

        targetEulerAngles.x = ClampAngleX(targetEulerAngles.x, cameraBottomClamp, cameraTopClamp);
        targetEulerAngles.y = ClampAngleY(targetEulerAngles.y, float.MinValue, float.MaxValue);

        cameraTarget.rotation = Quaternion.Euler(targetEulerAngles.x, targetEulerAngles.y, 0f);
        _lastLookRotation = cameraTarget.rotation;

        float ClampAngleX(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -180f)
            {
                lfAngle += 360f;
            }
            if (lfAngle > 180f)
            {
                lfAngle -= 360f;
            }
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
        float ClampAngleY(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f)
            {
                lfAngle += 360f;
            }
            if (lfAngle > 360f)
            {
                lfAngle -= 360f;
            }
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }

    void CameraZoom()
    {
        _currentCameraTargetDistance = Mathf.Clamp(_currentCameraTargetDistance + Inputs.zoom, cameraNearDistance, cameraFarDistance);

        float currentDistance = _3rdPersonFollow.CameraDistance;

        if (currentDistance == _currentCameraTargetDistance)
        {
            return;
        }

        float lerpedZoomValue = Mathf.Lerp(currentDistance, _currentCameraTargetDistance, cameraZoomSmooth * Time.deltaTime);

        _3rdPersonFollow.CameraDistance = lerpedZoomValue;
    }
}
