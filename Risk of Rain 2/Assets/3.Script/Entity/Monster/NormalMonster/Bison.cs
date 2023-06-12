using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Bison : Entity
{
    [SerializeField] private MonsterData _DataBison;
    [SerializeField] private Transform _BisonMouthTransform;
    [SerializeField] private GameObject _attackColider;
    private Vector3 _destination;
    private Vector3 _previousPosition;

    private Animator _animator;
    private Rigidbody _bisonRigidbody;
    private NavMeshAgent _navMeshAgent; //Nav Mesh Agent
    private GameObject _player;
    private Transform _playertag;

    private readonly float _rotateSpeed = 3f;

    private bool _isAttack;
    private bool _isFindTarget;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        this._animator = this.GetComponent<Animator>();
        TryGetComponent(out _bisonRigidbody);
        TryGetComponent(out _animator);
    }

    private void Start()
    {
        _playertag = GameObject.FindGameObjectWithTag("Player").transform;
        _previousPosition = transform.position;
        _player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(forStart_co());
    }

    private void Update()
    {
        if (!IsDeath)
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
                    transform.LookAt(_playertag);
                }
                else if (_navMeshAgent.remainingDistance <= 150f) // 플레이어가 인식 범위 내에 있는지 확인
                {
                    _animator.SetBool("isATK", false);
                    _animator.SetBool("isRun", true);
                }
                else
                {
                    _animator.SetBool("isATK", false);
                    _animator.SetBool("isRun", true);
                }
            }
            else
            {
                _isFindTarget = false;
                _navMeshAgent.ResetPath();
            }
            _navMeshAgent.SetDestination(_BisonMouthTransform.position);

          // if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
          // {
          //     _animator.SetBool("isRun", false);
          //     LookAtTarget();
          // }
          // else
          // {
          //
          // }
        }
    }

    private IEnumerator forStart_co()
    {
        while (true)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
            yield return null;
            _navMeshAgent.SetDestination(_player.transform.position);
        }
    }
    private void LookAtTarget()
    {
        Quaternion lookDirection = Quaternion.LookRotation(_BisonMouthTransform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * _rotateSpeed);
    }
    protected override void OnEnable()
    {
        setup(_DataBison);
        base.OnEnable();

        _attackColider.SetActive(false);
    }

    private void setup(MonsterData data)
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
        // Debug.Log(Time.time); // time before wait
        if (_navMeshAgent == null)
        {
            Debug.Log("??");
        }
        _animator.SetBool("isATK", true);
        _animator.SetBool("isRun", false);
        yield return new WaitForSeconds(2.05f);
        LookAtTarget();
        Debug.Log(Time.time); // time after wait
        _animator.SetBool("isATK", false);
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
