using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Entity
{
    [HideInInspector]
    public string Name;
    [HideInInspector]
    public float Mass;
    [HideInInspector]
    public float CriticalChance;
    [HideInInspector]
    public float MaxJumpCount;
    [SerializeField] private SurvivorsData _survivorsData;

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
