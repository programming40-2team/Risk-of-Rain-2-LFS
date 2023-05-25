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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Cube")
        {
            Debug.Log("맞기는 함");
        }
        if(other.CompareTag("Monster"))
        {
            other.GetComponent<Entity>().OnDamage(10);
        }
        _bulletPool.ReturnObject(this.gameObject);
    }
}
