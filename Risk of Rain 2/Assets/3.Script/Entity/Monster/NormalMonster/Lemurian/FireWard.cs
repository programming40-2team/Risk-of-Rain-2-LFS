using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWard : MonoBehaviour
{
    private Lemurian _lemurian;
    [SerializeField] private GameObject __lemurianObject;
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
        _lemurian.FireWardPool.ReturnObject(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject != __lemurianObject)
        {
            if(col.gameObject.CompareTag("Player"))
            {
                Debug.Log("플레이어 아야");
                col.GetComponent<Entity>().OnDamage(_damage);
            }
            // 사라지는 효과 있다면 여기
            DeleteFireWard();
        }
    }
}
