using UnityEngine;

public class BulletProjectile : Projectile
{
    protected override void InitializeProjectile()
    {
<<<<<<< HEAD
        TryGetComponent(out _bulletRigidbody);
        _bulletPool = FindObjectOfType<ObjectPool>();
    }


    private void OnEnable()
    {
        //_bulletRigidbody.rotation = Quaternion.identity;
        //float _speed = 40f;
        //_bulletRigidbody.velocity = transform.forward * _speed;
    }

    public void Shoot()
    {
        float _speed = 100f;
        _bulletRigidbody.velocity = transform.forward * _speed;
=======
        _projectilePoolName = "BulletPool";
        _projectileSpeed = 120f;
        base.InitializeProjectile();
>>>>>>> feature/Player
    }

    private void OnTriggerEnter(Collider other)
    {
<<<<<<< HEAD
        if (other.name == "Cube")
        {
            Debug.Log("맞기는 함");
        }
        if (other.CompareTag("Monster"))
=======
        if(other.CompareTag("Monster"))
>>>>>>> feature/Player
        {
            other.GetComponent<Entity>().OnDamage(10);
        }
        _projectileObjectPool.ReturnObject(this.gameObject);
    }
}
