using System.Collections;
using UnityEngine;
/// <summary>
/// 플레이어 스테이터스 클래스, 기본 변수는 Entity에 있고 플레이어만 가지는 변수는 여기서 선언
/// </summary>
public class PlayerStatus : Entity
{
    private Animator _playerAnimator;
    private PlayerInput _playerInput;
    public SurvivorsData _survivorsData;
    //Survivors Data에서 가져올 변수
    public string Name { get; private set; }
    public float Mass { get; private set; }
    public float CriticalChance { get; set; }
    public int MaxJumpCount { get; set; }

    //Survivors Data와 상관없는 고정 변수
    public int Level { get; private set; } = 1;
    public float Exp { get; private set; } = 100f;
    private float _currentExp;
    public float CurrentExp
    {
        get { return _currentExp; }
        set
        {
            _currentExp = value;
            if (_currentExp > Exp)
            {
                LevelUp();
                Level++;
                _currentExp = Exp - _currentExp;
                Exp *= 1.55f;
                Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerHpChange, this);
                Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerExpChange, this);
            }

        }
    }
    public float ChanceBlockDamage { get; set; }

    private void Awake()
    {
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerInput);
    }


    protected override void OnEnable()
    {
        InitStatus();
        StartCoroutine(RegenerateHealth_co());
        Health = MaxHealth;
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerHpChange, this);
        OnDeath -= ToDeath;
        OnDeath += ToDeath;
    }

    private void InitStatus()
    {
        Name = _survivorsData.Name;
        MaxHealth = _survivorsData.MaxHealth;
        MaxHealthAscent = _survivorsData.HealthAscent;
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

    public void AddMaxHealth(float addHealth)
    {

        MaxHealth += addHealth;
        OnHeal(addHealth);
    }
    public void OnHeal(float heal)
    {
        Health += heal;
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerHpChange, this);
    }
    public override void OnDamage(float damage)
    {
        if (!GetBlockChanceResult())
        {
            base.OnDamage(damage);
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerHpChange, this);
        }
    }

    protected override void RegenerateHealth()
    {
        base.RegenerateHealth();
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerHpChange, this);
    }

    public bool GetBlockChanceResult()
    {
        bool result = false;
        if (Random.Range(1, 101) <= ChanceBlockDamage)
        {
            //ChanceBlockDamage 내가 값을 설정해주면 될듯! -KYS
            result = true;
        }
        return result;
    }

    public float GetCriticalChanceResult()
    {
        float result = 1;
        if (Random.Range(1, 101) <= CriticalChance)
        {
            result = 2;
        }
        return result;
    }
    public void IncreaseExp(float exp)
    {
        CurrentExp += exp;
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerExpChange, this);
    }

    private void LevelUp()
    {
        MaxHealth += MaxHealthAscent;
        HealthRegen += HealthRegenAscent;
        Damage += DamageAscent;
    }

    private void ToDeath()
    {
        _playerAnimator.SetTrigger("Die");
        _playerInput.enabled = false;
        StartCoroutine(nameof(ShowResult_co));
    }
    private IEnumerator ShowResult_co()
    {
        yield return new WaitForSeconds(2.0f);
        Managers.Game.IsClear = false;
    }
}
