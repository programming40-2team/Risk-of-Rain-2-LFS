using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseRoundProjectile : Projectile
{
    private WaitForSeconds _phaseRoundLifeTime = new WaitForSeconds(20f);
    private float _damageAscent = 0.4f;

    protected override void InitializeProjectile()
    {
        _projectilePoolName = "PhaseRoundPool";
        _projectileSpeed = 60f;
        _damageCoefficient = 3.0f;
        base.InitializeProjectile();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(EndPhaseRound_co());
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out _entity))
        {
            float criticalCoefficient = _playerStatus.GetCriticalChanceResult();
            _entity.OnDamage(_damage * _damageCoefficient * criticalCoefficient);
            _damageCoefficient += _damageAscent;
            if(criticalCoefficient == 2)
            {
                //크리티컬 성공
            }
        }
    }

    private IEnumerator EndPhaseRound_co()
    {
        yield return _phaseRoundLifeTime;
        _damageCoefficient = 3.0f;
        _projectileObjectPool.ReturnObject(this.gameObject);
    }
}
