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

    private Vector3 _dir;

    private void OnEnable()
    {
        _beetleQueen = FindObjectOfType<BeetleQueen>();
        _beetleQueenObject = _beetleQueen.gameObject;
        _dir = new Vector3(_playerTransform.position.x - _beetleQueenMouthTransform.position.x, // 기준이 될 방향 벡터
            _playerTransform.position.y - _beetleQueenMouthTransform.position.y,
            _playerTransform.position.z - _beetleQueenMouthTransform.position.z).normalized;
        StartCoroutine(Shoot_co());

    }

    private IEnumerator Shoot_co() // 발사
    {
        float time = 0;
        while(time < 5f)
        {
            transform.position += _beetleQueen._dir * _shootingSpeed * Time.deltaTime;
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
