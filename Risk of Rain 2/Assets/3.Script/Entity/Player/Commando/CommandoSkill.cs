using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandoSkill : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Animator _playerAnimator;
    private Transform _cameraTransform;

    [SerializeField] private GameObject _leftMuzzle;
    [SerializeField] private GameObject _rightMuzzle;
    private Coroutine _attackCoroutine;

    //aiming
    private float _aimY;
    private RaycastHit _aimHit;

    //DoubleTap
    private ObjectPool _bulletObjectPool;
    private bool _isRight;
    private WaitForSeconds _doubleTapDelay = new WaitForSeconds(0.167f);
    private bool _isShooting;

    private void Awake()
    {
        TryGetComponent(out _playerInput);
        TryGetComponent(out _playerAnimator);
        _bulletObjectPool = FindObjectOfType<ObjectPool>();
    }
    private void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        //if (_playerInput.Mouse1)
        //{
        //    if (!_isShooting)
        //    {
        //        if (_attackCoroutine != null)
        //        {
        //            StopCoroutine(DoubleTap_co());
        //        }
        //        _attackCoroutine = StartCoroutine(DoubleTap_co());
        //    }
        //}
        //if (_playerInput.Mouse1 && !_isShooting)
        //{
        //    DoubleTap();
        //}
        //else
        //{
        //    StopDoubleTap();
        //}
        if(Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out _aimHit, Mathf.Infinity))
        {
            _aimY = _aimHit.point.y - transform.position.y;
            _playerAnimator.SetFloat("Aim", _aimY);
        }

        if (Input.GetKey(KeyCode.T))
        {
            _attackCoroutine ??= StartCoroutine(test_co());
        }
        else
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
            _playerAnimator.SetBool("DoubleTap", false);
        }
        _playerAnimator.SetBool("DoubleTap", true);
    }

    private IEnumerator test_co()
    {
        _playerAnimator.SetBool("DoubleTap", true);
        yield return _doubleTapDelay;
        _attackCoroutine = StartCoroutine(test_co());
    }

    private IEnumerator DoubleTap_co()
    {
        _isShooting = true;
        _isRight = !_isRight;
        _playerAnimator.SetBool("DoubleTap", true);
        Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit _hit, Mathf.Infinity);
        GameObject _bullet = _bulletObjectPool.GetObject();
        if (!_isRight)
        {
            _bullet.transform.position = _leftMuzzle.transform.position;
        }
        else
        {
            _bullet.transform.position = _rightMuzzle.transform.position;
        }
        StartCoroutine(Shoot_co(_bullet, _hit));
        yield return _doubleTapDelay;
        _isShooting = false;
        _playerAnimator.SetBool("DoubleTap", false);
    }
    private void DoubleTap()
    {
        Debug.Log("´õºí ÅÇ");
        StartCoroutine(CheckShooting_co());
        _isRight = !_isRight;
        _playerAnimator.SetBool("DoubleTap", true);
        Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit _hit, Mathf.Infinity);
        GameObject _bullet = _bulletObjectPool.GetObject();
        if (!_isRight)
        {
            _bullet.transform.position = _leftMuzzle.transform.position;
        }
        else
        {
            _bullet.transform.position = _rightMuzzle.transform.position;
        }
        StartCoroutine(Shoot_co(_bullet, _hit));

    }
    private void StopDoubleTap()
    {
        _playerAnimator.SetBool("DoubleTap", false);
        _isRight = false;
    }
    private IEnumerator CheckShooting_co()
    {
        _isShooting = true;
        yield return _doubleTapDelay;
        _isShooting = false;
    }
    private IEnumerator Shoot_co(GameObject bullet, RaycastHit hit)
    {
        bullet.GetComponent<Rigidbody>().MovePosition(hit.point);
        yield return null;
    }
}
