using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/MonsterData", fileName = "monsterData")]
public class MonsterData : ScriptableObject
{
    public float MaxHealth = 2100f;
    public float Damage = 25f;
    public float MoveSpeed = 6f;
    public float Amor = 20f;
    public float MaxHealthAscent = 630f;
    public float DamageAscent = 5f;
    public float HealthRegen = 0f;
    public float RegenAscent = 0f;
}
