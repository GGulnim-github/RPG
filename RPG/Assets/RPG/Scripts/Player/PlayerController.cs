using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [field: SerializeField] public PlayerStateMachine StateMachine { get; private set; }
    
    public PlayerInputs Inputs { get; private set; }
    public PlayerStat Stat { get; private set; }
    public PlayerAttack Attack { get; private set; }

    public Animator Animator { get; private set; }
    public CharacterController CharacterController { get; private set; }

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

    [NonSerialized] public bool isGrounded = true;
    [NonSerialized] public bool isAttack = false;
    [NonSerialized] public bool isDie = false;

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
        Instance = this;

        Inputs = GetComponent<PlayerInputs>();
        Stat = GetComponent<PlayerStat>();
        Attack = GetComponent<PlayerAttack>();

        Animator = GetComponent<Animator>();
        CharacterController = GetComponent<CharacterController>();

        handSword.SetActive(false);
        upperChestSword.SetActive(true);

        Stat.Initialize(2);
    }

    private void Start()
    {
        StateMachine = new PlayerStateMachine(this);
    }

    private void Update()
    {
        CheckGround();
        StateMachine.Update();
        Move();
    }

    private void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }

    void CheckGround()
    {
        if (verticalVelocity > 0.0f)
        {
            isGrounded = false;
            verticalVelocity += gravity * Time.deltaTime;
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
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    void Move()
    {
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
        CharacterController.Move(targetSpeed * Time.deltaTime * targetDirection.normalized + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
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

    public void UpdateTargetRotation()
    {
        if (Inputs.move != Vector2.zero)
        {
            targetRotation = Mathf.Atan2(Inputs.moveDirection.x, Inputs.moveDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        }
    }
}
