using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class gip : Entity
{
    [SerializeField] private MonsterData _DataGip;
    [SerializeField] private GameObject _attackColider;
    [SerializeField] float _gipForce = 0f;
    [SerializeField] GameObject _gipPrepeb = null;
    [SerializeField] Vector3 _gipOffset = Vector3.zero;
    private Vector3 _destination;
    private Vector3 _previousPosition;

    private Animator _animator;
    private Rigidbody _GeepRigidbody;
    private NavMeshAgent _navMeshAgent; //Nav Mesh Agent

    private GameObject _player;
    private Transform _targets;

    private bool _isAttack;
    private bool _isFindTarget;
    private bool _isDead;

    private float _damageCoefficient = 1.5f;

    private void Awake()
    {
        this._animator = this.GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        TryGetComponent(out _GeepRigidbody);
        TryGetComponent(out _animator);
        // StartCoroutine(Roaming_co());
    }

    private void Start()
    {
        // StartCoroutine(UpdateTargetCoroutine_co());
        _previousPosition = transform.position;
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        //Todo 거리 네비메쉬로 수정 navmeshAgent.reamainingDistance
        if (_navMeshAgent.remainingDistance <= 150f)
        {
            _isFindTarget = true;
            // 적과 대상 사이의 거리 계산
            if (_navMeshAgent.remainingDistance <= 5f) // 플레이어가 공격 범위 내에 있는지 확인
            {
                //Debug.Log("접근");
                if (!_isAttack)
                {
                    StartCoroutine(Attack_co());
                }
            }
            else if (_navMeshAgent.remainingDistance <= 150f) // 플레이어가 인식 범위 내에 있는지 확인
            {
                _animator.SetBool("Targeton", true);
                _animator.SetBool("isATK", false);
                _animator.SetBool("isRun", true);
            }
            else
            {
                _animator.SetBool("isATK", false);
                _animator.SetBool("isRun", true);
            }

            _navMeshAgent.SetDestination(_player.transform.position);

        }
        else
        {
            _isFindTarget = false;
            if (_navMeshAgent.enabled)
            {
                _navMeshAgent.ResetPath();
            }
        }

    }

    protected override void OnEnable()
    {
        SetUp(_DataGip);
        base.OnEnable();
        _attackColider.SetActive(false);
    }

    private void SetUp(MonsterData data)
    {
        MaxHealth = data.MaxHealth;
        Damage = data.Damage;
        MoveSpeed = data.MoveSpeed;
        Armor = data.Amor;
        MaxHealthAscent = data.MaxHealthAscent;
        DamageAscent = data.DamageAscent;
        HealthRegen = data.HealthRegen;
        HealthRegenAscent = data.RegenAscent;
    }


    IEnumerator Attack_co()
    {
        _isAttack = true;
        Debug.Log(Time.time); // time before wait
        if (_navMeshAgent == null)
        {
            //Debug.Log("??");
        }

        _animator.SetBool("isAttack", true);
        _animator.SetBool("isRun", false);
        yield return new WaitForSeconds(2.2f);
        Debug.Log(Time.time); // time after wait
        _animator.SetBool("isAttack", false);
        _animator.SetBool("isRun", true);
        _isAttack = false;
    }
    private void OnAttackColider()
    {
        _attackColider.SetActive(true);
    }

    public override void Die()
    {
        base.Die();
        Managers.Resource.Destroy(gameObject);
    }
}


