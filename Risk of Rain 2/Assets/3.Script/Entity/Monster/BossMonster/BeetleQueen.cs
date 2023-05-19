using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleQueen : Entity
{
    [SerializeField] private MonsterData _beetleQueenData;

    private Entity targetEntity;

    public ObjectPool objectPool;
    public List<GameObject> _acidList;

    private Animator _beetleQueenAnimator;
    private AudioSource _beetleQueenAudioSource;
    private AudioClip _hitSound;

    [Header("Transforms")]
    [SerializeField] private Transform _beetleQueenMouth;
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
        _acidList = new List<GameObject>();
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

    // 산성담즙 생성
    public void CreateAcidBile()
    {
        for(int i = 0; i < 6; i++)
        {
            GameObject obj = objectPool.GetObject();
            obj.transform.position = _beetleQueenMouth.position;
            _acidList.Add(obj);
        }
    }

    public void DeleteAcidBile()
    {
        for(int i = 0; i < 6; i++)
        {
            if (_acidList.Count > 0)
            {
                objectPool.ReturnObject(_acidList[0]);
                _acidList.RemoveAt(0);
            }
        }
    }
}