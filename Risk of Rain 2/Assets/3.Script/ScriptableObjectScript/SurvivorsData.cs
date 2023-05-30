using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SurvivorsData", fileName = "survivorsData")]
public class SurvivorsData : ScriptableObject
{
    public string Name;
    public float MaxHealth;
    public float Damage;
    public float HealthRegen;
    public float Armor;
    public float MoveSpeed;
    public float Mass;
    public float CriticalChance;
    public int MaxJumpCount;

    //레벨에 따른 증가량들

    public float HealthAscent;
    public float DamageAscent;
    public float HealthRegenAscent;
}
