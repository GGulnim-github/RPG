using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] public PlayerStateMachine StateMachine { get; private set; }
    public PlayerInputs Inputs { get; private set; }

    public Animator Animator { get; private set; }
    public CharacterController CharacterController { get; private set; }

    public Vector3 AnimatorDeltaPosition { get; private set; }
    public Vector3 AnimatorVelocity { get; private set; }

    [Header("Player")]
    public float walkSpeed = 1.0f;
    public float runSpeed = 2.2f;
    public float dashSpeed = 6.0f;
    public float jumpHeight = 1.0f;
    public float gravity = -15f;
    public float rotationSmoothTime = 0.12f;
    public float terminalVelocity = -53.0f;

    [HideInInspector] public float targetSpeed;
    [HideInInspector] public float targetRotation;
    [HideInInspector] public float rotationVelocity;
    [HideInInspector] public float verticalVelocity;

    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public bool isAttack = false;

    [Space(5)]
    [Header("Ground")]
    public float groundedOffset = -0.14f;
    public float groundedRadius = 0.28f;
    public LayerMask groundLayers;

    [Space(5)]
    [Header("Camera")]
    public Transform cameraTransform;

    [Space(5)]
    [Header("Equip")]
    public GameObject handSword;
    public GameObject upperChestSword;

    private void Awake()
    {
        Inputs = GetComponent<PlayerInputs>();

        Animator = GetComponent<Animator>();
        CharacterController = GetComponent<CharacterController>();

        handSword.SetActive(false);
        upperChestSword.SetActive(true);
    }

    private void Start()
    {
        StateMachine = new PlayerStateMachine(this);
    }

    private void Update()
    {
        CheckGround();
        StateMachine.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }

    private void OnAnimatorMove()
    {
        AnimatorDeltaPosition = Animator.deltaPosition;
        AnimatorVelocity = Animator.velocity;
    }

    private void CheckGround()
    {
        if (verticalVelocity > 0.0f)
        {
            isGrounded = false;
            return;
        }

        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers,
            QueryTriggerInteraction.Ignore);

        if (isGrounded)
        {
            if (verticalVelocity < 0.0f)
            {
                verticalVelocity = -2f;
            }
        }
    }

    public void EquipSword()
    {
        upperChestSword.SetActive(false);
        handSword.SetActive(true);
    }

    public void UnequipSword()
    {
        upperChestSword.SetActive(true);
        handSword.SetActive(false);
    }
}
