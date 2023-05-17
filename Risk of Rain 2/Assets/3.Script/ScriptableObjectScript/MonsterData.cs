using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/MonsterData", fileName = "monsterData")]
public class MonsterData : ScriptableObject
{
    public float Damage = 25f;
    public float Speed = 6f;
    public float Defense = 20f;
    public float MaxHealthAscent = 630f;
    public float DamageAscent = 5f;
    public float HealthRecovery = 0f;
    public float RecoveryAscent = 0f;
}
