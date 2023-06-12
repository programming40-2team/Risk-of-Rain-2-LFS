using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Geep : Entity
{
    [SerializeField] private MonsterData _DataGeep;
    [SerializeField] private GameObject _gip;
    [SerializeField] private GameObject _attackColider;
    private Vector3 _destination;
    private Vector3 _previousPosition;

    private Animator _animator;
    private Rigidbody _GeepRigidbody;
    private NavMeshAgent _navMeshAgent; //Nav Mesh Agent
    private GameObject _player;

    private bool _isAttack;
    private bool _isFindTarget;

    private void Awake()
    {
        this._animator = this.GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        TryGetComponent(out _GeepRigidbody);
        TryGetComponent(out _animator);
    }

    private void Start()
    {
        _previousPosition = transform.position;
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            OnDamage(10f);
            Debug.Log(Health);
        }
        // //Todo 거리 네비메쉬로 수정 navmeshAgent.reamainingDistance
        if (!IsDeath)
        {

            if (_navMeshAgent.remainingDistance <= 150f)
            {
                _isFindTarget = true;
                // 적과 대상 사이의 거리 계산
                if (_navMeshAgent.remainingDistance <= 10f) // 플레이어가 공격 범위 내에 있는지 확인
                {
                    //Debug.Log("접근");
                    if (!_isAttack)
                    {
                        StartCoroutine(Attack_co());
                    }
                }
                else if (_navMeshAgent.remainingDistance <= 150f) // 플레이어가 인식 범위 내에 있는지 확인
                {
                    _animator.SetBool("isAttack", false);
                    _animator.SetBool("isRun", true);
                }
                else
                {
                    _animator.SetBool("isAttack", false);
                    _animator.SetBool("isRun", true);
                }

                _navMeshAgent.SetDestination(_player.transform.position);

            }
            else
            {
                _isFindTarget = false;
                _navMeshAgent.ResetPath();
            }
        }

    }

    protected override void OnEnable()
    {
        SetUp(_DataGeep);
        base.OnEnable();

        OnDeath -= ToDeath;
        OnDeath += ToDeath;

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
        if (_navMeshAgent == null)
        {
            //Debug.Log("??");
        }
        Debug.Log("코루틴");

        _animator.SetBool("isAttack", true);
        _animator.SetBool("isRun", false);
        yield return new WaitForSeconds(2.2f);
       // Debug.Log(Time.time); // time after wait
        _animator.SetBool("isAttack", false);
        _animator.SetBool("isRun", true);
        _isAttack = false;

    }

    private void ToDeath()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);  //니가 정하삼
        GameObject _enemy = Managers.Resource.Instantiate($"{_gip.name}");
        _enemy.GetComponent<NavMeshAgent>().Warp(spawnPos);
        _enemy = Managers.Resource.Instantiate($"{_gip.name}");
        _enemy.GetComponent<NavMeshAgent>().Warp(spawnPos);

        Managers.Resource.Destroy(gameObject);
        
        //죽었을 때
        //serializefield로 gip 하나 넣고 아래 Instantiate 두 번 써서 두마리뽑고. 자기 Destroy
        // Instantiate(소환할 대상, 소환 위치, 소환 방향)// 소환
        //죽었을 때, 두마리 뽑고 본체 사라지면 될 삘
    }
    private void OnAttackColider()
    {
        _attackColider.SetActive(true);
    }
}