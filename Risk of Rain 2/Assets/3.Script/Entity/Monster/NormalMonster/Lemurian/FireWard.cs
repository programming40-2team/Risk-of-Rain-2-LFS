using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWard : MonoBehaviour
{
    private Lemurian _lemurian;
    [SerializeField] private GameObject _lemurianObject;
    private float _shootingSpeed = 45f;
    private float _damage = 0; // 100%

    private void OnEnable()
    {
        _lemurian = FindObjectOfType<Lemurian>();
        StartCoroutine(Shoot_co());
    }

    IEnumerator Shoot_co()
    {
        float time = 0;
        while (time < 5f)
        {
            transform.position += transform.forward * _shootingSpeed * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;
        }
        DeleteFireWard();
    }

    private void DeleteFireWard()
    {
        Debug.Log("사라졌다");
        _lemurian.FireWardPool.ReturnObject(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject != _lemurianObject)
        {
            if(col.gameObject.CompareTag("Player"))
            {
                Debug.Log("플레이어 아야");
                col.GetComponent<Entity>().OnDamage(_damage);
            }
            else
            {
                Debug.Log("벽이나 바닥에 닿음");
            }
            DeleteFireWard();
        }
    }
}
