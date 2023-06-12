using System.Collections;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [Header("도착점")]
    [SerializeField] private Transform _goalTransform; // 도착점
    [Header("시작점")]
    [SerializeField] private Transform _startTransform; // 시작점
    //private BeetleQueen _beetleQueen;
    //[SerializeField] private GameObject _beetleQueenObject;

    private Vector3 _startPos;
    private Vector3 _endPos;

    private float _maxHeight;
    private float _elapsedTime;
    private float _dat;

    private float _tx;
    private float _ty;
    private float _tz;

    private float _g = 9.8f;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Shoot_co(col.gameObject));
        }
    }

    private void Set()
    {
        _startPos = _startTransform.position + new Vector3(0, 2, 0);
        _endPos = _goalTransform.position + new Vector3(0, 2, 0);
        _maxHeight = _goalTransform.position.y + 7f;

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

    private IEnumerator Shoot_co(GameObject obj)
    {
        yield return null;
        Set();
        while (Vector3.SqrMagnitude(obj.transform.position - _goalTransform.position) >= 1f)
        {
            _elapsedTime += Time.deltaTime * 2;

            float tx = _startPos.x + _tx * _elapsedTime;
            float ty = _startPos.y + _ty * _elapsedTime - 0.5f * _g * _elapsedTime * _elapsedTime;
            float tz = _startPos.z + _tz * _elapsedTime;

            Vector3 tpos = new Vector3(tx, ty, tz);

            //transform.LookAt(tpos);
            obj.transform.position = tpos;
            Debug.Log(Vector3.SqrMagnitude(obj.transform.position - _goalTransform.position));
            yield return null;
        }
        obj.transform.position = _goalTransform.position;
    }
}
