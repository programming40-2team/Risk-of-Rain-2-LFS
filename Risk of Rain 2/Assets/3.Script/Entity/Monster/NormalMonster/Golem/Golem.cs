using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Golem : Entity
{
    [SerializeField] MonsterData _golemData;
    private NavMeshAgent _golemAgent;
    private Animator _golemAnimator;

    private Transform _targetTransform;
    private readonly float _rotateSpeed = 2f;
    private bool _isSpawn = false;

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
    private Coroutine _warningLaserCoroutine = null;

    // 근접 공격 관련
    [SerializeField] private Transform _clapZone;
    [SerializeField] private GameObject _clapEffect;
    private readonly float _seismicSlamCooldown = 3f;
    private float _seismicSlamCooldownRemain = 0f;

    private MonsterHpBar _myHpBar;

    private void Awake()
    {
        TryGetComponent(out _golemAgent);
        TryGetComponent(out _golemAnimator);

        _targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _golemAnimator.speed = 0.0f;

        _myHpBar = GetComponentInChildren<MonsterHpBar>();
        _myHpBar.gameObject.SetActive(false);

    }

    protected override void OnEnable()
    {
        InitStatus();
        InitEffect();
        base.OnEnable();
        OnDeath -= ToDeath;
        OnDeath += ToDeath;
        StartCoroutine(CheckNearPlayer_co());
    }

    private void Update()
    {
        if(_isSpawn && !IsDeath)
        {
            Aiming();
            Move();
            CheckAllCooldown();
            Attack();
        }
    }

    public override void OnDamage(float damage)
    {
        if(_isSpawn)
        {
            _myHpBar.gameObject.SetActive(true);
            base.OnDamage(damage);
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

    private IEnumerator CheckNearPlayer_co()
    {
        _golemAgent.isStopped = true;
        while (true)
        {
            _golemAgent.SetDestination(_targetTransform.position);
            yield return null;
            if (_golemAgent.remainingDistance <= 60f)
            {
                _golemAnimator.speed = 1f;
            }
            if (_golemAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f &&
                _golemAnimator.GetCurrentAnimatorStateInfo(0).IsName("Spawn"))
            {
                _golemAnimator.SetTrigger("Spawn");
                _isSpawn = true;
                _golemAgent.isStopped = false;
                break;
            }
        }
    }

    private void Aiming()
    {
        if (Physics.Raycast(_golemAimOrigin.transform.position, _golemAimOrigin.transform.forward, out _aimHit, Mathf.Infinity
            , (-1) - (1 << LayerMask.NameToLayer("Monster"))))
        {
            _aimRotation = Quaternion.LookRotation(_targetTransform.position - _golemAimOrigin.transform.position);
            _golemAimOrigin.transform.rotation = Quaternion.Slerp(_golemAimOrigin.transform.rotation, _aimRotation, Time.deltaTime * _aimSpeed);
        }
    }

    private void Move()
    {
        _golemAgent.SetDestination(_targetTransform.position);
        if (_laserAttackCooldownRemain > 0f && _warningLaserCoroutine == null)
        {
            _golemAgent.stoppingDistance = 10f;
        }
        else
        {
            _golemAgent.stoppingDistance = 30f;
        }

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
        if (_laserAttackCooldownRemain <= 0f && _golemAgent.remainingDistance >= 10f && _golemAgent.remainingDistance <= 40f)
        {
            StartCoroutine(LaserAttack_co());
        }
        if (_seismicSlamCooldownRemain <= 0f && _golemAgent.remainingDistance <= 8f)
        {
            StartCoroutine(SeismicSlam_co());
        }
    }

    private IEnumerator LaserAttack_co()
    {
        _laserAttackCooldownRemain = _laserAttackCooldown;
        _warningLaserCoroutine = StartCoroutine(WarningLaser_co());
        yield return _laserChargeTime;
        Vector3 explosionPos = _aimHit.point;
        if (_warningLaserCoroutine != null)
        {
            StopCoroutine(_warningLaserCoroutine);
            _warningLaserCoroutine = null;
        }
        _golemLaser.enabled = false;
        _chargeEffect.SetActive(false);
        _explosionEffect.transform.position = explosionPos;
        _explosionEffect.SetActive(true);
    }

    private IEnumerator WarningLaser_co()
    {
        _chargeEffect.SetActive(true);
        _golemLaser.enabled = true;
        while (true)
        {
            _golemLaser.SetPosition(0, _golemAimOrigin.transform.position);
            _golemLaser.SetPosition(1, _aimHit.point);
            yield return null;
        }

    }

    private IEnumerator SeismicSlam_co()
    {
        _seismicSlamCooldownRemain = _seismicSlamCooldown;
        _golemAnimator.SetTrigger("Smack");
        yield return null;
    }

    private void SeismicSlamEffect()
    {
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

    private void ToDeath()
    {
        _golemAnimator.SetTrigger("Die");
        StopAllCoroutines();
        InitEffect();
        _golemAgent.ResetPath();
        _myHpBar.gameObject.SetActive(false);
        Managers.Resource.Destroy(gameObject);
    }
}
