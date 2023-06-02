using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Imp : Entity
{
    [SerializeField] MonsterData _impData;
    private NavMeshAgent _impAgent;
    private Animator _impAnimator;

    private Transform _targetTransform;
    private readonly float _rotateSpeed = 2f;

    private float _attackCooltime = 1.0f;
    private bool isAttack = false;

    private void Awake()
    {
        TryGetComponent(out _impAgent);
        TryGetComponent(out _impAnimator);

        _targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected override void OnEnable()
    {
        InitStatus();
        base.OnEnable();
    }

    private void InitStatus()
    {
        MaxHealth = _impData.MaxHealth;
        Damage = _impData.Damage;
        MoveSpeed = _impData.MoveSpeed;
        Armor = _impData.Amor;
        MaxHealthAscent = _impData.MaxHealthAscent;
        DamageAscent = _impData.DamageAscent;
        HealthRegen = _impData.HealthRegen;
        HealthRegenAscent = _impData.RegenAscent;
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        _impAgent.SetDestination(_targetTransform.position);

        if (_impAgent.remainingDistance <= _impAgent.stoppingDistance)
        {
           
            _impAnimator.SetBool("Run", false);
            LookAtTarget();
        }
        else
        {
            _impAnimator.SetBool("Run", true);
        }
    }
    private void LookAtTarget()
    {
        Quaternion lookDirection = Quaternion.LookRotation(_targetTransform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * _rotateSpeed);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(TryGetComponent(out Entity entity))
            {
               
                if (!isAttack)
                {
                    StartCoroutine(nameof(Attack_co), entity);
                }
            }
            else
            {
                Debug.Log("Player Entity컴포넌트를 가져오지 못함");
            }
        }
    }

    private IEnumerator Attack_co(Entity entity)
    {
        isAttack = true;
        _impAnimator.SetBool("Run", false);
        _impAnimator.SetBool("DoubleSlash", true);
        entity.OnDamage(Damage);
        yield return new WaitForSeconds(_attackCooltime);
        isAttack = false;
        _impAnimator.SetBool("Run", true);
    }
    public override void OnDamage(float damage)
    {
        if (!IsDeath)
        {
            //.Play();
            //.PlayOneShot(hitSound);
            _impAnimator.SetTrigger("Hit");

        }

        base.OnDamage(damage);
    }
    public override void Die()
    {
        base.Die();
        _impAnimator.SetTrigger("Die");

        //Collider[] colls = GetComponents<Collider>();
        //foreach (Collider col in colls)
        //{
        //    col.enabled = false;
        //}

        //_navMeshAgent.isStopped = true;
        //_navMeshAgent.enabled = false;

        Debug.Log("레무리안 죽는 사운드 넣을거면 여기");
    }

}
