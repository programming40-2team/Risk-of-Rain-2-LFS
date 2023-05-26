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

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<Entity>().OnDamage(10f);
        }
    }

    private IEnumerator EndPhaseRound_co()
    {
        yield return _phaseRoundLifeTime;
        _projectileObjectPool.ReturnObject(this.gameObject);
    }
}
