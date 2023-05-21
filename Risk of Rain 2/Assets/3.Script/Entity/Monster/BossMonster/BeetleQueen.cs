using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleQueen : Entity
{
    // TODO : 난이도에 따라 MaxHealth 증가시키기
    [SerializeField] private MonsterData _beetleQueenData;

    private Entity targetEntity;

    public ObjectPool objectPool;

    private Animator _beetleQueenAnimator;
    private AudioSource _beetleQueenAudioSource;
    private AudioClip _hitSound;

    [Header("Transforms")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _beetleQueenMouthTransform;

    public Vector3 _dir;
    public Vector3[] _dirArr = new Vector3[6];

    private bool hasTarget
    {
        get
        {
            if (targetEntity != null && !targetEntity.IsDeath)
            {
                return true;
            }

            return false;
        }
    }
    private void Awake()
    {
        TryGetComponent(out _beetleQueenAnimator);
        objectPool = FindObjectOfType<ObjectPool>();
    }
    
    protected override void OnEnable()
    {
        SetUp(_beetleQueenData);
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

    public override void OnDamage(float damage)
    {
        if (!IsDeath)
        {
            //hitEffect.transform.SetPositionAndRotation(hitposition, Quaternion.LookRotation(hitnormal)); / ㅁ?ㄹ
            //hitEffect.Play();
            //_beetleQueenAudio.PlayOneShot(hitSound);
        }

        base.OnDamage(damage);
    }

    public override void Die()
    {
        base.Die();
        //_beetleQueenAnimator.SetTrigger("Die");
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

    private void SetDirection()
    {
        _dir = new Vector3(_playerTransform.position.x - _beetleQueenMouthTransform.position.x, // 기준이 될 방향 벡터
            _playerTransform.position.y - _beetleQueenMouthTransform.position.y,
            _playerTransform.position.z - _beetleQueenMouthTransform.position.z).normalized;

        for(int i = 0; i < _dirArr.Length; i++)
        {
            _dirArr[i] = new Vector3(0, 0, 0);
        }
    }
    // 산성담즙 생성
    private void CreateAcidBile()
    {
        for(int i = 0; i < 6; i++)
        {
            GameObject obj = objectPool.GetObject();
            obj.transform.position = _beetleQueenMouthTransform.position;

        }
    }

    //private IEnumerator AcidBile_co()
    //{
    //    CreateAcidBile();
    //    yield return new WaitForSeconds(4f); // 나중에 수정
    //    DeleteAcidBile();
    //}

    public void StartAcidBileSkill()
    {
        SetDirection();
        CreateAcidBile();
    }
}