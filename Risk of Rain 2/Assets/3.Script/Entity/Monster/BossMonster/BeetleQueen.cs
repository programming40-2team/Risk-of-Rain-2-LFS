using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleQueen : Entity
{
    [SerializeField] private MonsterData _beetleQueenData;
    private Animator _beetleQueenAnimator;

    private Transform _playerTransform; // 플레이어 향해 공격하기 때문에 필요

    private void Awake()
    {
        TryGetComponent<Animator>(out _beetleQueenAnimator);
    }

    protected override void OnEnable()
    {
        SetUp(_beetleQueenData);
        base.OnEnable();
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
