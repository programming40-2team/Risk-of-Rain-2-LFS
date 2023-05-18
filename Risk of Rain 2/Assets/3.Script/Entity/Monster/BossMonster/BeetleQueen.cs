using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleQueen : Entity
{
    [SerializeField] private MonsterData _beetleQueenData;

    private Entity targetEntity;

    [Header("효과")]
    public Animator _beetleQueenAnimator;
    private AudioSource _beetleQueenAudioSource;
    private AudioClip _hitSound;

    private Transform _playerTransform; // 플레이어 향해 공격하기 때문에 필요
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
        TryGetComponent<Animator>(out _beetleQueenAnimator);
    }

    protected override void OnEnable()
    {
        SetUp(_beetleQueenData);
        base.OnEnable();

        Debug.Log(Damage);
        Debug.Log(MoveSpeed);
        Debug.Log(Armor);
        Debug.Log(MaxHealthAscent);
        Debug.Log(DamageAscent);
        Debug.Log(HealthRegen);
        Debug.Log(HealthRegenAscent);
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
        _beetleQueenAnimator.SetTrigger("Die");
        base.Die();
    }

    private void SetUp(MonsterData data)
    {
        Damage = data.Damage;
        MoveSpeed = data.MoveSpeed;
        Armor = data.Amor;
        MaxHealthAscent = data.MaxHealthAscent;
        DamageAscent = data.DamageAscent;
        HealthRegen = data.HealthRegen;
        HealthRegenAscent = data.RegenAscent;
    }


}