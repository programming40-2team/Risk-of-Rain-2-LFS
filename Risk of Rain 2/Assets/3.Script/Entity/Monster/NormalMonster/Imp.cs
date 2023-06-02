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

    [SerializeField] private Transform _clapZone;


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
    private void Attack()
    {
        if (Random.Range(0, 10) % 2 == 0)
        {

        }
        else
        {

        }
    }



}
