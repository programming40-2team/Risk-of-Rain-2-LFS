using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Transform _playerTransform;
    private Transform _beetleQueenButtTransform;

    private Vector3 _startPos;
    private Vector3 _endPos;

    private float _maxHeight;
    private float _elapsedTime;
    private float _dat;

    private float _tx;
    private float _ty;
    private float _tz;

    private float _g = 9.8f;

    private void OnEnable()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        _beetleQueenButtTransform = GameObject.FindGameObjectWithTag("BeetleQueenButt").transform;
        Set();
        StartCoroutine(Shoot_co());
    }

    private void Set()
    {
        _startPos = _beetleQueenButtTransform.position;
        _endPos = _playerTransform.position;
        _maxHeight = transform.position.y + 20f;
        
        float dh = _endPos.y - _startPos.y;
        float mh = _maxHeight - _startPos.y;

        _ty = Mathf.Sqrt(2 * _g * mh);

        float a = _g;
        float b = -2 * _ty;
        float c = 2 * dh;

        _dat = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);

        _tx = -(_startPos.x - _endPos.x) / _dat;
        _tz = -(_startPos.z - _endPos.z) / _dat;

        _elapsedTime = 0;
    }

    IEnumerator Shoot_co()
    {
        while(true)
        {
            _elapsedTime += Time.deltaTime * 2;

            float tx = _startPos.x + _tx * _elapsedTime;
            float ty = _startPos.y + _ty * _elapsedTime - 0.5f * _g * _elapsedTime * _elapsedTime;
            float tz = _startPos.z + _tz * _elapsedTime;

            Vector3 tpos = new Vector3(tx, ty, tz);

            transform.LookAt(tpos);
            transform.position = tpos;

            if (_elapsedTime >= _dat)
            {
                break;
            }
            yield return null;
        }
    }
}
