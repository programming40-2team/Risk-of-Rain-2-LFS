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
    [SerializeField]
    public float Health { get; protected set; }
    [SerializeField]
    public bool IsDeath { get; protected set; }
    public event Action OnDeath;

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