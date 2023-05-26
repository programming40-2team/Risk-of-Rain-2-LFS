using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody _bulletRigidbody;
    private ObjectPool _bulletPool;
    private void Awake()
    {
        TryGetComponent(out _bulletRigidbody);
        _bulletPool = FindObjectOfType<ObjectPool>();
    }

    public void Shoot()
    {
        float _speed = 130f;
        _bulletRigidbody.velocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            other.GetComponent<Entity>().OnDamage(10);
        }
        _bulletPool.ReturnObject(this.gameObject);
    }
}
