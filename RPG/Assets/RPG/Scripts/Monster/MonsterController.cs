using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class MonsterController : MonoBehaviour
{
    [field: SerializeField] public MonsterStateMachine StateMachine { get; private set; }

    public MonsterData data;
    public MonsterHUD hud;

    public Animator Animator { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }

    uint _currentHP;
    uint CurrentHP
    {
        get { return _currentHP; }
        set
        {
            _currentHP = value;
            hud.hpSlider.value = _currentHP;
            if (_currentHP == 0)
            {
                Debug.Log("Die");
            }
        }
    }

    public float rotationSmoothTime = 0.12f;

    public float idleExitMinTime = 1f;
    public float idleExitMaxTime = 3f;

    public float moveRadius;
    public float findRadius;
    public float chaseRadius;
    public float attackDistance;
    public float attackDelay;

    public LayerMask targetLayerMask;
    Vector3 _rayOffset = new Vector3(0, 0.1f, 0);

    [HideInInspector] public Vector3 spawnPos;
    [HideInInspector] public PlayerController target;
    [HideInInspector] public float rotationVelocity;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        hud.Initialize(data.Level, data.Name, data.HP);
        _currentHP = data.HP;
        hud.gameObject.SetActive(false);

        spawnPos = transform.position;
    }

    private void Start()
    {
        StateMachine = new MonsterStateMachine(this);
    }

    private void Update()
    {
        StateMachine.Update();
    }

    public void ReciveDamage(uint damage, PlayerController attackPlayer)
    {
        hud.gameObject.SetActive(true);

        if (hud.damageTransform != null)
        {
            HUDManager.Instance.PlayDamageText(hud.damageTransform, damage);
        }

        if (damage >= CurrentHP)
        {
            CurrentHP = 0;
        }
        else
        {
            CurrentHP -= damage;
        }

        if (StateMachine.CurrentStateName == MonsterStateName.Idle || StateMachine.CurrentStateName == MonsterStateName.Move)
        {
            target = attackPlayer;
            StateMachine.ChangeState(MonsterStateName.Chase);
        }

        Animator.CrossFadeInFixedTime("Damage", 0.1f);
    }

    public bool FindTarget()
    {
        target = null;
        
        float stepAngleSize = 360 / 36;
        for (int i = 0; i < 36; i++)
        {
            float angle = transform.eulerAngles.y - 360 / 2 + stepAngleSize * i;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            Ray ray = new(transform.position + _rayOffset, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, findRadius, targetLayerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.TryGetComponent(out target))
                {
                    if (IsPointWithinRadius(spawnPos, chaseRadius, target.transform.position))
                    {
                        return true;
                    }
                    target = null;
                }
            }
        }

        return false;
    }

    public bool IsPointWithinRadius(Vector3 center, float radius, Vector3 point)
    {
        Vector2 centerVector2 = new Vector2(center.x, center.z);
        Vector2 pointVector2 = new Vector2(point.x, point.z);
        float distance = Vector2.Distance(centerVector2, pointVector2);
        return distance <= radius;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(spawnPos + _rayOffset, Vector3.up, moveRadius, 1f);

            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawWireDisc(spawnPos + _rayOffset, Vector3.up, chaseRadius, 1f);
        }
        else
        {

            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position + _rayOffset, Vector3.up, moveRadius, 1f);

            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawWireDisc(transform.position + _rayOffset, Vector3.up, chaseRadius, 1f);
        }

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position + _rayOffset, Vector3.up, findRadius, 1f);
    }
#endif
}
