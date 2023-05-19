using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSkill : MonoBehaviour
{
    private BeetleQueen _beetleQueen;
    private GameObject _beetleQueenObject;
    private float _shootingSpeed = 7f;

    [Header("Transforms")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _beetleQueenMouthTransform;

    private Vector3 _dir;

    private void OnEnable()
    {
        _beetleQueen = FindObjectOfType<BeetleQueen>();
        _beetleQueenObject = _beetleQueen.gameObject;

        _dir = new Vector3(_playerTransform.position.x - _beetleQueenMouthTransform.position.x,
            _playerTransform.position.y - _beetleQueenMouthTransform.position.y,
            _playerTransform.position.z - _beetleQueenMouthTransform.position.z).normalized;
        StartCoroutine(Shoot_co());

    }

    private IEnumerator Shoot_co() // น฿ป็
    {
        while(true)
        {
            transform.position += _dir * _shootingSpeed * Time.deltaTime;
            yield return null;
        }
    }

    private void OnParticleCollision(GameObject col)
    {
        if (col.gameObject != _beetleQueenObject)
        {
            _beetleQueen.objectPool.ReturnObject(gameObject);
            _beetleQueen._acidList.Remove(gameObject);
        }
    }
}
