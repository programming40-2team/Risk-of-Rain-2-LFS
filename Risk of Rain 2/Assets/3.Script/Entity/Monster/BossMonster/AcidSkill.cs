using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSkill : MonoBehaviour
{
    private BeetleQueen _beetleQueen;
    private GameObject _beetleQueenObject;
    private float _shootingSpeed = 20f;
    private float _damage;

    [Header("Transforms")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _beetleQueenMouthTransform;

    private void OnEnable()
    {
        _beetleQueen = FindObjectOfType<BeetleQueen>();
        _beetleQueenObject = _beetleQueen.gameObject;
        StartCoroutine(Shoot_co());

    }

    private IEnumerator Shoot_co() // 발사
    {
        float time = 0;
        while(time < 5f)
        {
            transform.position += transform.forward * _shootingSpeed * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;
        }
        DeleteAcidBile();
    }

    // 산성담즙 풀에 반환
    private void DeleteAcidBile()
    {
        _beetleQueen.objectPool.ReturnObject(gameObject);
    }

    private void OnParticleCollision(GameObject col)
    {
        if (col.gameObject != _beetleQueenObject)
        {   
            if(col.gameObject.CompareTag("Player"))
            {
                col.gameObject.GetComponent<Entity>().OnDamage(_damage);
            }
            // TODO : 터져서 사라지는 효과 코루틴으로 넣기
            _beetleQueen.objectPool.ReturnObject(gameObject);
        }
    }
}
