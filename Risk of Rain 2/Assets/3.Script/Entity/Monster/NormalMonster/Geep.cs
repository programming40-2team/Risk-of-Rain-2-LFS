using System.Collections;
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
        // //Todo �Ÿ� �׺�޽��� ���� navmeshAgent.reamainingDistance
        if (!IsDeath)
        {

            if (_navMeshAgent.remainingDistance <= 150f)
            {
                _isFindTarget = true;
                // ���� ��� ������ �Ÿ� ���
                if (_navMeshAgent.remainingDistance <= 5f) // �÷��̾ ���� ���� ���� �ִ��� Ȯ��
                {
                    //Debug.Log("����");
                    if (!_isAttack)
                    {
                        StartCoroutine(Attack_co());
                    }
                }
                else if (_navMeshAgent.remainingDistance <= 150f) // �÷��̾ �ν� ���� ���� �ִ��� Ȯ��
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
        Debug.Log("�ڷ�ƾ");

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
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);  //�ϰ� ���ϻ�
        GameObject _enemy = Managers.Resource.Instantiate($"{_gip.name}");
        _enemy.GetComponent<NavMeshAgent>().Warp(spawnPos);
        _enemy = Managers.Resource.Instantiate($"{_gip.name}");
        _enemy.GetComponent<NavMeshAgent>().Warp(spawnPos);

        Managers.Resource.Destroy(gameObject);

        //�׾��� ��
        //serializefield�� gip �ϳ� �ְ� �Ʒ� Instantiate �� �� �Ἥ �θ����̰�. �ڱ� Destroy
        // Instantiate(��ȯ�� ���, ��ȯ ��ġ, ��ȯ ����)// ��ȯ
        //�׾��� ��, �θ��� �̰� ��ü ������� �� ��
    }
    private void OnAttackColider()
    {
        _attackColider.SetActive(true);
    }
}