using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Imp : Entity
{
    [SerializeField] MonsterData _impData;
    private NavMeshAgent _impAgent;
    private Animator _impAnimator;

    private GameObject _target;

    private float _attackCooltime = 1.0f;
    private bool isAttack = false;
    private MonsterHpBar _myHpBar;

    private bool isBlank = false;

    //타격 범위도 COliider로 설정해서 몸통 주변 때리면 총알 먹힘

    enum EAttackType
    {
        Attack,
        DoubleSlash,

    }

    private void Awake()
    {
        TryGetComponent(out _impAgent);
        TryGetComponent(out _impAnimator);
        _myHpBar = GetComponentInChildren<MonsterHpBar>();
        _myHpBar.gameObject.SetActive(false);
        _target = GameObject.FindGameObjectWithTag("Player");

    }
    protected override void OnEnable()
    {
        InitStatus();
        base.OnEnable();
        _impAnimator.SetBool("Run", true);
        foreach (var colider in GetComponentsInChildren<Collider>())
        {
            colider.enabled = true;
        }
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
        Blank();
    }
    private void Blank()
    {
        if (Health / MaxHealth < 0.3f)
        {
            if (!isBlank)
            {
                StartCoroutine(nameof(Blink_co));
            }

        }
    }

    private IEnumerator Blink_co()
    {
        isBlank = true;
        _impAnimator.SetTrigger("BlinkStart");
        float currentClipLength = _impAnimator.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log("Blink  Start Effect ");
        yield return new WaitForSeconds(currentClipLength);
        SetOff();
        gameObject.SetRandomPositionSphere(3, 8, 0, _target.transform);
        yield return new WaitForSeconds(1.0f);
        SetOn();
        _impAnimator.SetTrigger("BlinkEnd");
        Debug.Log("Blink  End Effect ");
        yield return new WaitForSeconds(1.5f);
    }

    private void Move()
    {
        _impAgent.SetDestination(_target.transform.position);

        if (_impAgent.remainingDistance <= _impAgent.stoppingDistance)
        {

            _impAnimator.SetBool("Run", false);
            _impAgent.isStopped = true;
        }
        else
        {
            _impAnimator.SetBool("Run", true);
            _impAgent.isStopped = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Entity entity))
            {

                if (!isAttack)
                {
                    if (Util.Probability(30))
                    {
                        StartCoroutine(nameof(Attack_co), EAttackType.DoubleSlash);
                    }
                    else
                    {
                        StartCoroutine(nameof(Attack_co), EAttackType.Attack);
                    }
                }
            }
            else
            {
                Debug.Log("Player Entity컴포넌트를 가져오지 못함");
            }
        }
    }

    private IEnumerator Attack_co(EAttackType type)
    {
        isAttack = true;
        _impAnimator.SetBool("Run", false);
        switch (type)
        {
            case EAttackType.Attack:
                _impAnimator.SetBool("BaseAttack", true);
                yield return new WaitForSeconds(_attackCooltime);
                _impAnimator.SetBool("BaseAttack", false);
                break;
            case EAttackType.DoubleSlash:
                _impAnimator.SetBool("DoubleSlash", true);
                yield return new WaitForSeconds(_attackCooltime);
                _impAnimator.SetBool("DoubleSlash", false);
                break;
        }
        isAttack = false;
    }
    public void BaseAttack()
    {
        Debug.Log("Imp AttackEffect");
        if (GameObject.FindGameObjectWithTag("Player").TryGetComponent(out Entity entity))
        {
            entity.OnDamage(Damage);
        }
    }
    public void SpecialAttack()
    {
        Debug.Log("Imp SKill Effect");
        if (GameObject.FindGameObjectWithTag("Player").TryGetComponent(out Entity entity))
        {
            entity.OnDamage(2 * Damage);
        }
    }



    public override void OnDamage(float damage)
    {
        if (!IsDeath)
        {
            //.Play();
            //.PlayOneShot(hitSound);
            _impAnimator.SetTrigger("Hit");
            _myHpBar.gameObject.SetActive(true);
        }

        base.OnDamage(damage);
    }
    private IEnumerator Die_co()
    {
        IsDeath = false;
        _myHpBar.gameObject.SetActive(false);
        _impAnimator.SetTrigger("Die");
        foreach (var colider in GetComponentsInChildren<Collider>())
        {
            colider.enabled = false;
        }
        float currentClipLength = _impAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(currentClipLength);
        Managers.Resource.Destroy(gameObject);
    }
    public override void Die()
    {
        base.Die();
        if (IsDeath == true)
        {
            StartCoroutine(nameof(Die_co));
        }
        Debug.Log("레무리안 죽는 사운드 넣을거면 여기");
    }

    private void SetOff()
    {
        if (TryGetComponent(out CapsuleCollider capsule))
        {
            capsule.enabled = false;

        }
        transform.GetChild(0).gameObject.SetActive(false);
        _myHpBar.gameObject.SetActive(false);
    }
    private void SetOn()
    {
        if (TryGetComponent(out CapsuleCollider capsule))
        {
            capsule.enabled = true;

        }
        transform.GetChild(0).gameObject.SetActive(true);
        _myHpBar.gameObject.SetActive(true);
    }

}
