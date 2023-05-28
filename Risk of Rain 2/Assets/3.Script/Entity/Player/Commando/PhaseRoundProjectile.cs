using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseRoundProjectile : Projectile
{
    private WaitForSeconds _phaseRoundLifeTime = new WaitForSeconds(20f);

    protected override void InitializeProjectile()
    {
        _projectilePoolName = "PhaseRoundPool";
        _projectileSpeed = 60f;
        base.InitializeProjectile();
    }
    private void OnEnable()
    {
        StartCoroutine(EndPhaseRound_co());
    }
    
    private void OnTriggerEnter(Collider other)
    {
    }
    private IEnumerator EndPhaseRound_co()
    {
        yield return _phaseRoundLifeTime;
        _projectileObjectPool.ReturnObject(this.gameObject);
    }
}
