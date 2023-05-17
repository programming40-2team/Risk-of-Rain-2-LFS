using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // 최대체력 o
    // 체력 o
    // 공격력 Damage
    // 속도 Speed
    // 방어력 Defense
    // 레벨당 체력 상승치 MaxHealthAscent
    // 레벨당 공격력 상승치 DamageAscent

    // 체력 회복
    // 레벨당 체력 회복 상승치

    // MonsterData //
    // MaxHealth
    // Damage
    // Speed
    // Defense
    // MaxHealthAscent
    // DamageAscent

    // --------------------------------------
    public float MaxHealth; // 레벨에 따라 늘어남
    public float Health { get; protected set; }
    public bool IsDeath { get; protected set; }
    public event Action OnDeath;

    protected float _damage; // 공격력
    protected float _speed; // 속도
    protected float _defense; // 방어력
    protected float _maxHealthAscent; // 레벨당 체력 상승치
    protected float _damageAscent; // 레벨당 공격력 상승치
    protected float _healthRecovery;// 체력 회복량
    protected float _recoveryAscent;// 레벨당 체력 회복량

    protected virtual void OnEnable()
    {
        IsDeath = false;
        // MaxHealth = data.health;
        Health = MaxHealth;
    }

    /// <summary>
    /// 데미지 받을 때 사용
    /// </summary>
    /// <param name="damage"></param>
    public virtual void OnDamage(float damage)
    {
        float damageMultiplier = 1 - _defense / (100 + Mathf.Abs(_defense));
        damage += damageMultiplier;

        Health -= damage;

        if (Health <= 0 && !IsDeath)
        {
            Die();
        }
    }

    /// <summary>
    /// OnDamage 안에 들어있는 메소드 / OnDeath()실행 / 조건 : Health <=0 && !IsDeath
    /// </summary>
    public virtual void Die()
    {
        if (OnDeath != null)
        {
            OnDeath();
        }
        IsDeath = true;
    }
}