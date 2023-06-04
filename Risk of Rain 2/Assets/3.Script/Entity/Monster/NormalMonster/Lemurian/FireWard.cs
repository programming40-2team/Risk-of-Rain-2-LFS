using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWard : MonoBehaviour
{
    private Lemurian _lemurian;
    [SerializeField] private GameObject _lemurianObject;
    private float _shootingSpeed = 45f;
    private float _damage = 0;

    private void OnEnable()
    {
        _lemurian = FindObjectOfType<Lemurian>();
        StartCoroutine(Shoot_co());
        _damage = _lemurian.Damage; // 100% / 조건 걸어 줘야 함
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
        _lemurian.FireWardPool.ReturnObject(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject != _lemurianObject)
        {
            if(col.gameObject.TryGetComponent(out Entity en))
            {
                if(en.CompareTag("Player"))
                {
                    Debug.Log("플레이어가 레무리안의 FireWard에 맞음 가한 damage : " + _damage);
                    Debug.Log("플레이어 Hit Sound는 여기");
                    col.GetComponent<Entity>().OnDamage(_damage);
                    DeleteFireWard();
                }
            }
            else
            {
                Debug.Log("레무리안의 FireWard가 벽이나 바닥에 닿는 소리는 여기");
                DeleteFireWard();
            }
        }
    }
}
