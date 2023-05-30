using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemurian : Entity
{
    [SerializeField] private MonsterData _lemurianData;
    private GameObject _player;

    public ObjectPool FireWardPool;

    public Animator LemurianAnimator;

    private void Awake()
    {
        TryGetComponent(out LemurianAnimator);
        _player = GameObject.FindGameObjectWithTag("Player");
        FireWardPool = GameObject.Find("FireWardPool").GetComponent<ObjectPool>();
    }

    protected override void OnEnable()
    {
        SetUp(_lemurianData);
        base.OnEnable();
        Debug.Log("Health : " + Health);
        Debug.Log("IsDeath : " + IsDeath);
        Debug.Log("Damage : " + Damage);
        Debug.Log("MoveSpeed : " + MoveSpeed);
        Debug.Log("Armor : " + Armor);
        Debug.Log("MaxHealthAscent : " + MaxHealthAscent);
        Debug.Log("DamageAscent : " + DamageAscent);
        Debug.Log("HealthRegen : " + HealthRegen);
        Debug.Log("HealthRegenAscent : " + HealthRegenAscent);
    }

    private void SetUp(MonsterData data)
    {
        MaxHealth = data.MaxHealth;
        Damage = data.Damage;
        MoveSpeed = data.MoveSpeed;
        Armor = data.Amor;
        MaxHealthAscent = data.MaxHealthAscent;
        DamageAscent = data.DamageAscent;
        HealthRegen = data.HealthRegen;
        HealthRegenAscent = data.RegenAscent;
    }

    public override void OnDamage(float damage)
    {
        if (!IsDeath)
        {
            //.Play();
            //.PlayOneShot(hitSound);
        }

        base.OnDamage(damage);
    }

    public override void Die()
    {
        base.Die();
        LemurianAnimator.SetTrigger("Die");
    }

    /// <summary>
    /// 원거리에서는 플레이어와 거리를 두고 수평으로 이동하면서 100%의 피해를 지닌 불덩이를 발사
    /// </summary>
    public void FireWardSkill()
    {
        // Quaternion rot = Quaternion.LookRotation(_player.transform.position - _beetleQueenMouthTransform.position);
        // GameObject obj = FireWardPool.GetObject();
        // obj.transform.SetPositionAndRotation(_beetleQueenMouthTransform.position, Quaternion.Euler(0, -20f + 8 * i, 0) * rot);
        // obj 방향 플레이어 향하도록 설정해야함
    }

    /// <summary>
    /// 10m 이내로 근접하면 달려와서 할퀴기 공격을 해 200%의 피해를 입힘. 할퀴기 공격은 1초의 쿨타임이 존재
    /// </summary>
    public void ScratchSkill() // 이펙트가 있는지 없는지 모르겠음
    {

    }
}
