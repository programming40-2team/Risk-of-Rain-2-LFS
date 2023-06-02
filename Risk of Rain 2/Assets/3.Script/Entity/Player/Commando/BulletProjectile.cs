using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectile
{
    protected override void InitializeProjectile()
    {
        _projectilePoolName = "BulletPool";
        _projectileSpeed = 160f;
        _damageCoefficient = 1f;
        base.InitializeProjectile();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out _entity))
        {
            _entity.OnDamage(_damage *  _damageCoefficient * _playerStatus.GetCriticalChanceResult());
        }
        _projectileObjectPool.ReturnObject(gameObject);
    }
}