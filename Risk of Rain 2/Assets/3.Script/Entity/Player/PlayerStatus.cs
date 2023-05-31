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
    public float CriticalChance { get;  set; }
    public int MaxJumpCount { get; private set; }

    //Survivors Data와 상관없는 고정 변수
    public int Level { get; private set; }
    public float Exp { get; private set; } = 100f;
    public float CurrentExp { get; private set; }
    public float ChanceBlockDamage { get;  set; }

    protected override void OnEnable()
    {
        InitStatus();
        base.OnEnable();
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerHpChange, this);
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
    //직접 넣는 것보다 OnHeal, OnDamage로 넣는게 좋을 것 같아서 수정!
    public void OnHeal(float heal)
    {
        Health += heal;
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerHpChange, this);
    }
    public override void OnDamage(float damage)
    {
        if(GetBlockChanceResult())
        {
            base.OnDamage(damage);
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerHpChange, this);
        }
    }

    public bool GetBlockChanceResult()
    {
        bool result = false;
        if(Random.Range(1,101) <= ChanceBlockDamage)
        {
            //ChanceBlockDamage 내가 값을 설정해주면 될듯! -KYS
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
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerExpChange, this);
        }
    }
    public void IncreaseExp(float exp)
    {
        CurrentExp += exp;
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerExpChange, this);
    }
}