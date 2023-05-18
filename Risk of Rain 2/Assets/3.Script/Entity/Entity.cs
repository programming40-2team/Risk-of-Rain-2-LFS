using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 플레이어와 몬스터가 공유할 클래스,
/// 변수 : MaxHealth, Damage, MoveSpeed, Armor, MaxHealthAscent, DamageAscent, HealthRegen, HealthRegenAscent
/// </summary>
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
    [HideInInspector]
    public float MaxHealth; // 레벨에 따라 늘어남
    public float Health { get; protected set; }
    public bool IsDeath { get; protected set; }
    public event Action OnDeath;

    public float Damage { get; protected set; } // 공격력
    public float MoveSpeed { get; protected set; } // 속도
    public float Armor { get; protected set; } // 방어력
    public float MaxHealthAscent { get; protected set; } // 레벨당 체력 상승치
    public float DamageAscent { get; protected set; } // 레벨당 공격력 상승치
    public float HealthRegen { get; protected set; }// 체력 회복량
    public float HealthRegenAscent { get; protected set; }// 레벨당 체력 회복량

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
        float damageMultiplier = 1 - Armor / (100 + Mathf.Abs(Armor));
        damage *= damageMultiplier;

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