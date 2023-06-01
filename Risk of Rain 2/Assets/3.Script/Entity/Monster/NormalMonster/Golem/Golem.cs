using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public class Golem : Entity
{
    [SerializeField] MonsterData _golemData;
    private NavMeshAgent _golemAgent;
    private Animator _golemAnimator;

    private Transform _targetTransform;
    private readonly float _rotateSpeed = 2f;

    // 레이저 공격 스킬 관련
    [SerializeField] GameObject _golemAimOrigin;
    [SerializeField] LineRenderer _golemLaser;
    [SerializeField] GameObject _chargeEffect;
    [SerializeField] GameObject _explosionEffect;
    private RaycastHit _aimHit;
    private Quaternion _aimRotation;
    private readonly WaitForSeconds _laserChargeTime = new WaitForSeconds(3f);
    private readonly float _aimSpeed = 2f;
    private readonly float _laserAttackCooldown = 5f;
    private float _laserAttackCooldownRemain = 0f;

    // 근접 공격 관련
    [SerializeField] private Transform _clapZone;
    [SerializeField] private GameObject _clapEffect;
    private readonly float _seismicSlamCooldown = 3f;
    private float _seismicSlamCooldownRemain = 0f;

    private void Awake()
    {
        TryGetComponent(out _golemAgent);
        TryGetComponent(out _golemAnimator);

        _targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void OnEnable()
    {
        InitStatus();
        InitEffect();
        base.OnEnable();
    }

    private void Update()
    {
        Aiming();
        Move();
        CheckAllCooldown();
        Attack();

        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(LaserAttack_co());
        }
    }

    private void InitStatus()
    {
        MaxHealth = _golemData.MaxHealth;
        Damage = _golemData.Damage;
        MoveSpeed = _golemData.MoveSpeed;
        Armor = _golemData.Amor;
        MaxHealthAscent = _golemData.MaxHealthAscent;
        DamageAscent = _golemData.DamageAscent;
        HealthRegen = _golemData.HealthRegen;
        HealthRegenAscent = _golemData.RegenAscent;
    }

    private void InitEffect()
    {
        _golemLaser.positionCount = 2;
        _golemLaser.enabled = false;
        _chargeEffect.SetActive(false);
        _explosionEffect.SetActive(false);
        _clapEffect.SetActive(false);
    }
    private void Aiming()
    {
        if (Physics.Raycast(_golemAimOrigin.transform.position, _golemAimOrigin.transform.forward, out _aimHit, Mathf.Infinity))
        {
            _aimRotation = Quaternion.LookRotation(_targetTransform.position - _golemAimOrigin.transform.position);
            _golemAimOrigin.transform.rotation = Quaternion.Slerp(_golemAimOrigin.transform.rotation, _aimRotation, Time.deltaTime * _aimSpeed);
        }
    }

    private void Move()
    {
        _golemAgent.SetDestination(_targetTransform.position);

        if (_golemAgent.remainingDistance <= _golemAgent.stoppingDistance)
        {
            _golemAnimator.SetBool("Run", false);
            LookAtTarget();
        }
        else
        {
            _golemAnimator.SetBool("Run", true);
        }
    }
    private void LookAtTarget()
    {
        Quaternion lookDirection = Quaternion.LookRotation(_targetTransform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * _rotateSpeed);
    }
 
    private void Attack()
    {
        if(_laserAttackCooldownRemain <= 0f && _golemAgent.remainingDistance >= 10f && _golemAgent.remainingDistance <=40f)
        {
            StartCoroutine(LaserAttack_co());
        }
        if (_seismicSlamCooldownRemain <= 0f && _golemAgent.remainingDistance <= 10)
        {
            StartCoroutine(SeismicSlam_co());
        }
    }

    private IEnumerator LaserAttack_co()
    {
        Debug.Log("레이저");
        _laserAttackCooldownRemain = _laserAttackCooldown;
        Coroutine chargelaser = StartCoroutine(WarningLaser_co());
        yield return _laserChargeTime;
        Vector3 explosionPos = _aimHit.point;
        if(chargelaser != null) 
        {
            StopCoroutine(chargelaser); 
        }
        _golemLaser.enabled = false;
        _chargeEffect.SetActive(false);
        _explosionEffect.transform.position = explosionPos;
        _explosionEffect.SetActive(true);
    }

    private IEnumerator WarningLaser_co()
    {
        
            Debug.Log("여기가 안되나");
            _chargeEffect.SetActive(true);
            _golemLaser.enabled = true;
            while (true)
            {
                Debug.Log(_golemAimOrigin.transform.position);
                _golemLaser.SetPosition(0, _golemAimOrigin.transform.position);
                _golemLaser.SetPosition(1, _aimHit.point);
                yield return null;
            }
        
    }

    private IEnumerator SeismicSlam_co()
    {
        Debug.Log("클랩");
        _seismicSlamCooldownRemain = _seismicSlamCooldown;
        _golemAnimator.SetTrigger("Smack");
        yield return null;
    }

    private void SeismicSlamEffect()
    {
        Debug.Log("애니메이션 이벤트");
        _clapEffect.SetActive(true);
    }
  
    private void CheckCooldown(ref float skillCooldownRemain)
    {
        if (skillCooldownRemain > 0)
        {
            skillCooldownRemain -= Time.deltaTime;
        }
        else
        {
            skillCooldownRemain = 0f;
        }
    }

    private void CheckAllCooldown()
    {
        CheckCooldown(ref _laserAttackCooldownRemain);
        CheckCooldown(ref _seismicSlamCooldownRemain);
    }
}