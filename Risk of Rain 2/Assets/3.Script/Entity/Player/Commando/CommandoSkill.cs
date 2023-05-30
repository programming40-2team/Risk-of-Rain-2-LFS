using System.Collections;
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
    [SerializeField] private GameObject _leftMuzzleEffect;
    [SerializeField] private GameObject _rightMuzzleEffect;


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

        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out _aimHit, Mathf.Infinity,
            (-1) - (1 << LayerMask.NameToLayer("Player"))))
        {

        }
        //에임 애니메이션
        //if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out _aimHit, Mathf.Infinity))
        //{
        //    _aimY = _aimHit.point.y - transform.position.y;
        //    _playerAnimator.SetFloat("Aim", _aimY);
        //}

        if (_playerInput.Mouse1)
        {
            _attackCoroutine ??= StartCoroutine(DoubleTap_co());
        }
        else
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
            _playerAnimator.SetBool("DoubleTap", false);
            _isRight = false;
        }
    }

    private IEnumerator DoubleTap_co()
    {
        _playerAnimator.SetBool("DoubleTap", true);
        GameObject bullet = _bulletObjectPool.GetObject();
        Vector3 bulletDirection;
        if (!_isRight)
        {
            bulletDirection = _aimHit.point - _leftMuzzle.transform.position;
            bullet.transform.position = _leftMuzzle.transform.position;
            _leftMuzzleEffect.SetActive(true);
        }
        else
        {
            bulletDirection = _aimHit.point - _rightMuzzle.transform.position;
            bullet.transform.position = _rightMuzzle.transform.position;
            _rightMuzzleEffect.SetActive(true);
        }
        bullet.transform.rotation = Quaternion.LookRotation(bulletDirection, Vector3.up);
        bullet.GetComponent<BulletProjectile>().Shoot();
        _isRight = !_isRight;
        yield return _doubleTapDelay;
        _attackCoroutine = StartCoroutine(DoubleTap_co());
    }
}