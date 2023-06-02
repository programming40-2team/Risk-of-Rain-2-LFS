using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectile
{
    protected override void InitializeProjectile()
    {
        _projectilePoolName = "BulletPool";
        _projectileSpeed = 160f;
        base.InitializeProjectile();
    }

    private void OnTriggerEnter(Collider other)
    {
        _projectileObjectPool.ReturnObject(gameObject);
    }
}