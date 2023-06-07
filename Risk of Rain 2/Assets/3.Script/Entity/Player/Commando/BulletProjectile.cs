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
        float criticalCoefficient = _playerStatus.GetCriticalChanceResult();
        if (other.gameObject.transform.parent.root.TryGetComponent(out _entity))
        {
            _entity.OnDamage(_damage * _damageCoefficient * criticalCoefficient);

            if (criticalCoefficient==2)
            {
                PrintDamage(other.gameObject, Define.EDamageType.Cirtical);
            }
            else
            {
                PrintDamage(other.gameObject);
            }

        }
        _projectileObjectPool.ReturnObject(gameObject);
    }
}
