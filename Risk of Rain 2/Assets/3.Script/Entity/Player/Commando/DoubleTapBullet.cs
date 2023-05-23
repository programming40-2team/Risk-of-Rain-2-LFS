using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTapBullet : MonoBehaviour
{
    private ObjectPool _bulletPool;
    private void OnEnable()
    {
        _bulletPool = FindObjectOfType<ObjectPool>();
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<Entity>().OnDamage(10);
        }
        _bulletPool.ReturnObject(this.gameObject);
    }
}