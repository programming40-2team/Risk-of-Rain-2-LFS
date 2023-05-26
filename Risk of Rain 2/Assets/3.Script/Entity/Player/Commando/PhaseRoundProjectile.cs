using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseRoundProjectile : Projectile
{
    private ParticleSystem _phaseRoundParticle;
    //private void Awake()
    //{
    //    TryGetComponent(out _phaseRoundParticle);
    //}
    //private void OnEnable()
    //{
    //    _phaseRoundParticle.Play();
    //}
    protected override void InitializeProjectile()
    {
        _projectilePoolName = "PhaseRoundPool";
        _projectileSpeed = 60f;
        base.InitializeProjectile();
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("ÆÄÆ¼Å¬!");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<Entity>().OnDamage(10f);
        }
        if (other.gameObject.layer == _environmentLayer)
        {
            _projectileObjectPool.ReturnObject(this.gameObject);
        }
    }
}
