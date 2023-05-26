using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectile
{
    protected override void InitializeProjectile()
    {
        _projectilePoolName = "BulletPool";
        _projectileSpeed = 120f;
        base.InitializeProjectile();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            other.GetComponent<Entity>().OnDamage(10);
        }
        _projectileObjectPool.ReturnObject(this.gameObject);
    }
}
