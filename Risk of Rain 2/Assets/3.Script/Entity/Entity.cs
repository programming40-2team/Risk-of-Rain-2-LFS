using System;
using System.Collections;
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

    private float _health;
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = Mathf.Clamp(value, 0, MaxHealth);
        }
    }


    public bool IsDeath { get; protected set; }
    public event Action OnDeath;

    public float Damage { get; set; } // 공격력
    public float MoveSpeed { get; set; } // 속도
    public float Armor { get; set; } // 방어력
    public float MaxHealthAscent { get; protected set; } // 레벨당 체력 상승치
    public float DamageAscent { get; protected set; } // 레벨당 공격력 상승치
    public float HealthRegen { get; set; }// 체력 회복량
    public float HealthRegenAscent { get; protected set; }// 레벨당 체력 회복량
    private WaitForSeconds _healthRegenDelay = new WaitForSeconds(1f);

    protected static int _difficulty = 0;
    protected virtual void OnEnable()
    {
        IsDeath = false;
        // MaxHealth = data.health;

        MaxHealth += MaxHealthAscent * _difficulty;
        Health = MaxHealth;
        Damage += DamageAscent * _difficulty;
        HealthRegen += HealthRegenAscent * _difficulty;
        StartCoroutine(RegenerateHealth_co());
    }

    private void Start()
    {
        Managers.Event.DifficultyChange -= SetDifficulty;
        Managers.Event.DifficultyChange += SetDifficulty;
    }

    private void SetDifficulty(int difficulty)
    {
        Debug.Log(string.Format("난이도 설정 {0}", difficulty));
        _difficulty = difficulty;
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
        if (!gameObject.CompareTag("Player"))
        {
            Managers.ItemApply.ExcuteInSkills();
            Managers.Game.MonsterDamaged += damage;

        }
        else
        {
            Managers.Game.PlayerAttackedDamage += damage;

        }

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
        if (!gameObject.CompareTag("Player"))
        {
            Managers.ItemApply.ExcuteAfterSkills(gameObject.transform);

            Managers.Game.KillCount++;
            Managers.Game.Gold += 100 + UnityEngine.Random.Range(15, 25 + 10 * (int)Managers.Game.Difficulty);
            Debug.Log("골드 잠시 치트모드로 올려둠");

            int _randomvalue = UnityEngine.Random.Range(1, 3);
            for (int i = 0; i < _randomvalue; i++)
            {
                Managers.Resource.Instantiate("Gold").SetRandomPositionSphere(3, 3, 3, gameObject.transform);
                Managers.Resource.Instantiate("Exp").SetRandomPositionSphere(2, 5, 2, gameObject.transform);

            }

        }
        if (OnDeath != null)
        {
            OnDeath();
        }
        IsDeath = true;
    }

    protected IEnumerator RegenerateHealth_co()
    {
        while (true)
        {
            RegenerateHealth();
            yield return _healthRegenDelay;
        }
    }

    protected virtual void RegenerateHealth()
    {
        Health += HealthRegen;
    }
}