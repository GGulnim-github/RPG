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
    public Collider Collider { get; private set; }
    public SkinnedMeshRenderer SkinnedMeshRenderer { get; private set; }
    Material originalMaterial;
    public Material dissolveMaterial;

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
                NavMeshAgent.enabled = false;

                SkinnedMeshRenderer.material = dissolveMaterial;
                dissolve = 0;
                SkinnedMeshRenderer.material.SetFloat("_Dissolve", dissolve);
                StateMachine.ChangeState(MonsterStateName.Die);

                Invoke(nameof(StartDissolve), 1f);
            }
        }
    }

    public float rotationSmoothTime = 0.12f;

    public float idleExitMinTime = 1f;
    public float idleExitMaxTime = 3f;
    public float reviveTime = 5f;

    public float moveRadius;
    public float findRadius;
    public float chaseRadius;
    public float attackDistance;
    public float attackDelay;

    public LayerMask targetLayerMask;
    Vector3 _rayOffset = new Vector3(0, 0.1f, 0);

    [HideInInspector] public Vector3 firstSpawnPos;
    [HideInInspector] public PlayerController target;
    [HideInInspector] public float rotationVelocity;
    [HideInInspector] public Vector3? lastDestination;

    float dissolve = 0;
    bool isDissolve = false;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Collider = GetComponent<Collider>();
        SkinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originalMaterial = SkinnedMeshRenderer.material;

        firstSpawnPos = transform.position;

        Collider.isTrigger = false;
        CurrentHP = data.HP;
        hud.gameObject.SetActive(false);

        transform.eulerAngles = new Vector3(0f, Random.Range(0, 360f),0f);
    }

    private void Start()
    {
        StateMachine = new MonsterStateMachine(this);
    }

    private void Update()
    {
        StateMachine.Update();

        if (isDissolve == true)
        {
            dissolve += Time.deltaTime;
            SkinnedMeshRenderer.material.SetFloat("_Dissolve", dissolve);
        }
    }

    public void StartDissolve()
    {
        isDissolve = true;
    }

    public void Revive()
    {
        isDissolve = false;
        NavMeshAgent.enabled = true;
        SkinnedMeshRenderer.material = originalMaterial;

        if (lastDestination == null)
        {
            transform.position = firstSpawnPos;
        }
        else
        {
            transform.position = lastDestination.Value;
        }

        Collider.isTrigger = false;
        CurrentHP = data.HP;
        hud.gameObject.SetActive(false);
        StateMachine.ChangeState(MonsterStateName.Idle);
        transform.eulerAngles = new Vector3(0f, Random.Range(0, 360f), 0f);
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
            return;
        }
        else
        {
            CurrentHP -= damage;
        }

        target = attackPlayer;

        if (StateMachine.CurrentStateName != MonsterStateName.Attack && StateMachine.CurrentStateName != MonsterStateName.Damage)
        {
            StateMachine.ChangeState(MonsterStateName.Damage);
        }

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
                    if (IsPointWithinRadius(firstSpawnPos, chaseRadius, target.transform.position))
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
            UnityEditor.Handles.DrawWireDisc(firstSpawnPos + _rayOffset, Vector3.up, moveRadius, 1f);

            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawWireDisc(firstSpawnPos + _rayOffset, Vector3.up, chaseRadius, 1f);
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
