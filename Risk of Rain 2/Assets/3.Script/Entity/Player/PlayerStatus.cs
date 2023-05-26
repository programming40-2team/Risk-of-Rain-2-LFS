using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 플레이어 스테이터스 클래스, 기본 변수는 Entity에 있고 플레이어만 가지는 변수는 여기서 선언
/// </summary>
public class PlayerStatus : Entity
{
    [SerializeField] private SurvivorsData _survivorsData;
    //Survivors DAta에서 가져올 변수
    [HideInInspector] public string Name;
    [HideInInspector] public float Mass;
    [HideInInspector] public float CriticalChance;
    [HideInInspector] public int MaxJumpCount;
    //Survivors Data와 상관없는 고정 변수
    [HideInInspector] public int Level;
    [HideInInspector] public float Exp;
    [HideInInspector] public float Gold;

    protected override void OnEnable()
    {
        InitStatus();
        base.OnEnable();
    }

    private void InitStatus()
    {
        Name = _survivorsData.Name;
        MaxHealth = _survivorsData.MaxHealth;
        Damage = _survivorsData.Damage;
        HealthRegen = _survivorsData.HealthRegen;
        Armor = _survivorsData.Armor;
        MoveSpeed = _survivorsData.MoveSpeed;
        Mass = _survivorsData.Mass;
        CriticalChance = _survivorsData.CriticalChance;
        MaxJumpCount = _survivorsData.MaxJumpCount;
    }
}
