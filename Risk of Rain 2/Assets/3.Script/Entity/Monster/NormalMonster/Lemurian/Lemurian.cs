using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lemurian : Entity
{
    [SerializeField] public MonsterData _lemurianData;
    private Animator _lemurianAnimator;
    private GameObject _player;
    
    [Header("추적대상 레이어")]
    public LayerMask TargetLayer;
    private Entity _targetEntity;
    private NavMeshAgent _navMeshAgent;

    public ObjectPool FireWardPool;

    [Header("Transforms")]
    [SerializeField] private Transform _lemurianMouthTransform;

    private float[] _skillCoolDownArr = new float[2];
    private bool[] _isSkillRun = new bool[2];


    //HpBar 
    private MonsterHpBar _myHpBar;
    private bool _hasTarget
    {
        get
        {
            if (_targetEntity != null && !_targetEntity.IsDeath)
            {
                return true;
            }

            return false;
        }
    }
    private void Awake()
    {
        TryGetComponent(out _navMeshAgent);
        TryGetComponent(out _lemurianAnimator);
        _player = GameObject.FindGameObjectWithTag("Player");
        _lemurianMouthTransform = GameObject.FindGameObjectWithTag("LemurianMouth").transform;
        FireWardPool = GameObject.Find("FireWardPool").GetComponent<ObjectPool>();
        _skillCoolDownArr[0] = 2f;
        _skillCoolDownArr[1] = 1f;

        for (int i = 0; i < _isSkillRun.Length; i++)
        {
            _isSkillRun[i] = false;
        }


        _myHpBar = GetComponentInChildren<MonsterHpBar>();
        _myHpBar.gameObject.SetActive(false);
    }

    protected override void OnEnable()
    {
        SetUp(_lemurianData);
        base.OnEnable();
        Debug.Log("Health : " + Health);
        Debug.Log("IsDeath : " + IsDeath);
        Debug.Log("Damage : " + Damage);
        Debug.Log("MoveSpeed : " + MoveSpeed);
        Debug.Log("Armor : " + Armor);
        Debug.Log("MaxHealthAscent : " + MaxHealthAscent);
        Debug.Log("DamageAscent : " + DamageAscent);
        Debug.Log("HealthRegen : " + HealthRegen);
        Debug.Log("HealthRegenAscent : " + HealthRegenAscent);
    }

    private void Start()
    {
        StartCoroutine(UpdateTargetPosition_co());
    }

    private void Update()
    {
        _lemurianAnimator.SetBool("IsRun", _hasTarget);
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
        _navMeshAgent.speed = data.MoveSpeed;
    }

    public override void OnDamage(float damage)
    {
        if (!IsDeath)
        {
            //.Play();
            //.PlayOneShot(hitSound);
            _lemurianAnimator.SetTrigger("Hit");
            _myHpBar.gameObject.SetActive(true);
        }

        base.OnDamage(damage);
    }

    public override void Die()
    {
        base.Die();
        _lemurianAnimator.SetTrigger("Die");

        Collider[] colls = GetComponents<Collider>();
        foreach (Collider col in colls)
        {
            col.enabled = false;
        }

        _navMeshAgent.isStopped = true;
        _navMeshAgent.enabled = false;

        StartCoroutine(Destroy_co());

        Debug.Log("레무리안 죽는 사운드 넣을거면 여기");
        _myHpBar.gameObject.SetActive(false);
    }

    private IEnumerator Destroy_co()
    {
        yield return new WaitForSeconds(5f);
        // 풀에 반환 시키기
    }

    /// <summary>
    /// 원거리에서는 플레이어와 거리를 두고 수평으로 이동하면서 100%의 피해를 지닌 불덩이를 발사
    /// </summary>
    public void FireWardSkill()
    {
        Quaternion rot = Quaternion.LookRotation(_player.transform.position - _lemurianMouthTransform.position);
        GameObject obj = FireWardPool.GetObject();
        obj.transform.SetPositionAndRotation(_lemurianMouthTransform.position, Quaternion.Euler(0, 0, 0) * rot);
    }

    /// <summary>
    /// 10m 이내로 근접하면 달려와서 할퀴기(bite) 공격을 해 200%의 피해를 입힘. 할퀴기 공격은 1초의 쿨타임이 존재
    /// </summary>
    public void BiteSkill() // 이펙트가 있는지 없는지 모르겠음
    {
        float damage = Damage * 2;
        if (Vector3.Distance(transform.position, _targetEntity.transform.position) <= 1f)
        {
            _player.GetComponent<Entity>().OnDamage(damage); // 200%
            Debug.Log("플레이어가 레무리안의 Bite에 맞음 가한 damage : " + damage);
        }
    }

    private IEnumerator UpdateTargetPosition_co()
    {
        while (!IsDeath)
        {
            if (_hasTarget)
            {
                Debug.Log("타겟이 있습니다.");
                _navMeshAgent.isStopped = false;
                _navMeshAgent.SetDestination(_targetEntity.transform.position);
                if(Vector3.Distance(transform.position, _targetEntity.transform.position) <= 10f)
                {
                    if(!_isSkillRun[1])
                    {
                        UseSkill(1);
                    }
                }
                else if(Vector3.Distance(transform.position, _targetEntity.transform.position) <= 50f)
                {
                    if (!_isSkillRun[0])
                    {
                        UseSkill(0);
                    }
                }
                else
                {
                    _targetEntity = null;
                }
            }
            else
            {
                Debug.Log("타겟이 없습니다.");
                _navMeshAgent.isStopped = true;

                Collider[] colls = Physics.OverlapSphere(transform.position, 30f, TargetLayer);

                for (int i = 0; i < colls.Length; i++)
                {
                    if (colls[i].TryGetComponent(out Entity en))
                    {
                        if (!en.IsDeath)
                        {
                            _targetEntity = en;
                            break;
                        }
                    }
                }
            }

            yield return null;
        }
    }

    private void UseSkill(int skillIndex)
    {
        StartCoroutine(UseSkill_co(skillIndex));
    }

    private IEnumerator UseSkill_co(int skillIndex)
    {
        switch (skillIndex)
        {
            case 0:
                _lemurianAnimator.SetTrigger("FireWard"); // 스킬은 애니메이터에 이벤트로 있음
                Debug.Log("불 뿜었다");
                _isSkillRun[skillIndex] = true;
                break;
            case 1:
                _lemurianAnimator.SetTrigger("Bite"); // 스킬은 애니메이터에 이벤트로 있음
                Debug.Log("물었다");
                _isSkillRun[skillIndex] = true;
                break;
        }
        yield return new WaitForSeconds(_skillCoolDownArr[skillIndex]); // 쿨타임만큼 기다리기
        _isSkillRun[skillIndex] = false; // 스킬 쿨타임 다 돌았음

    }
}
