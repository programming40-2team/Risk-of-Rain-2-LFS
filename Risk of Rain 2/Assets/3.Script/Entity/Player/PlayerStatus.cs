using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
/// <summary>
/// 플레이어 스테이터스 클래스, 기본 변수는 Entity에 있고 플레이어만 가지는 변수는 여기서 선언
/// </summary>
public class PlayerStatus : Entity
{
    public  SurvivorsData _survivorsData;
    //Survivors Data에서 가져올 변수
    public string Name { get; private set; }
    public float Mass { get; private set; }
    public float CriticalChance { get; private set; }
    public int MaxJumpCount { get; private set; }

    //Survivors Data와 상관없는 고정 변수
    public int Level { get; private set; }
    public float Exp { get; private set; } = 100f;
    public float CurrentExp { get; private set; }
    public float ChanceBlockDamage { get; private set; }

    protected override void OnEnable()
    {
        InitStatus();
        base.OnEnable();
    }

    private void Update()
    {
        CheckLevel();
    }

    private void InitStatus()
    {
        Name = _survivorsData.Name;
        MaxHealth = _survivorsData.MaxHealth;
        Damage = _survivorsData.Damage;
        DamageAscent = _survivorsData.DamageAscent;
        HealthRegen = _survivorsData.HealthRegen;
        HealthRegenAscent = _survivorsData.HealthRegenAscent;
        Armor = _survivorsData.Armor;
        MoveSpeed = _survivorsData.MoveSpeed;
        Mass = _survivorsData.Mass;
        CriticalChance = _survivorsData.CriticalChance;
        MaxJumpCount = _survivorsData.MaxJumpCount;
    }

    public override void OnDamage(float damage)
    {
        if(GetBlockChanceResult())
        {
            base.OnDamage(damage);
        }
    }

    private bool GetBlockChanceResult()
    {
        bool result = false;
        if(Random.Range(1,101) <= ChanceBlockDamage)
        {
            result = true;
        }
        return result;
    }
    
    private void CheckLevel()
    {
        if(CurrentExp >= Exp)
        {
            Level++;
            CurrentExp = 0f;
            Exp *= 1.55f;
            Managers.Event.PlayerExpUp?.Invoke();
        }
    }
    public void IncreaseExp(float exp)
    {
        CurrentExp += exp;
        Managers.Event.PlayerExpUp?.Invoke();
    }
}