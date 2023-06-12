using UnityEngine;

public class SpFireBullet : Projectile
{
    private ObjectPool _spFireEffectPool;
    protected override void InitializeProjectile()
    {
        _projectilePoolName = "SpFirePool";
        _projectileSpeed = 50f;
        _damageCoefficient = 1f;

        _spFireEffectPool = GameObject.Find("SpFireHitEffect").GetComponent<ObjectPool>();
        base.InitializeProjectile();
    }

    private void OnTriggerEnter(Collider other)
    {
        _spFireEffectPool.GetObject().transform.position = transform.position;
        float criticalCoefficient = _playerStatus.GetCriticalChanceResult();
        if (other.gameObject.transform.parent != null)
        {
            if (other.gameObject.transform.parent.root.TryGetComponent(out _entity))
            {
                _entity.OnDamage(_damage * _damageCoefficient * criticalCoefficient);
                if (criticalCoefficient == 2)
                {
                    PrintDamage(other.gameObject, Define.EDamageType.Cirtical);
                }
                else
                {
                    PrintDamage(other.gameObject);
                }
            }
        }
        else
        {
            if (other.TryGetComponent(out _entity))
            {
                _entity.OnDamage(_damage * _damageCoefficient * criticalCoefficient);
                if (criticalCoefficient == 2)
                {
                    PrintDamage(other.gameObject, Define.EDamageType.Cirtical);
                }
                else
                {
                    PrintDamage(other.gameObject);
                }
            }
        }
        _projectileObjectPool.ReturnObject(gameObject);
    }
}
