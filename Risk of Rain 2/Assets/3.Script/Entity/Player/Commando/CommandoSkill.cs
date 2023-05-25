using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using System;

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
    private float _centerAimY = 124.75f;
    private RaycastHit _aimHit;
    
    //DoubleTap (1번째 스킬 / 평타 / 노쿨)
    private ObjectPool _bulletObjectPool;
    private bool _isRight;
    private bool _isShooting;
    private WaitForSeconds _doubleTapDelay = new WaitForSeconds(0.167f);
    [SerializeField] private GameObject _leftMuzzleEffect;
    [SerializeField] private GameObject _rightMuzzleEffect;

    //PhaseRound (2번째 스킬)
    private float _phaseRoundCooldown = 3f;
    private float _phaseRoundCooldownRemain = 0f;

    //Tactical Dive (3번째 스킬)
    private float _tacticalDiveCooldown = 4f;
    private float _tacticalDiveCooldownRemain = 0f;

    //Suppressive Fire (4번째 스킬)
    private float _suppressiveFireCooldown = 9f;
    private float _suppressiveFireCooldownRemain = 0f;

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
        Aiming();
        CheckDoubleTap();
        CheckPhaseRound();
        CheckTacticalDive();
        CheckSuppressiveFire();
        CheckAllCooldown();
    }

    private void Aiming()
    {
        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out _aimHit, Mathf.Infinity,
            (-1) - (1 << LayerMask.NameToLayer("Player"))))
        {
            _aimY = _aimHit.point.y - transform.position.y - _centerAimY;
            _playerAnimator.SetFloat("Aim", _aimY / _centerAimY);
        }
    }

   
    private void CheckDoubleTap()
    {
        if (_playerInput.Mouse1)
        {
            if (_attackCoroutine == null)
            {
                if (!_isShooting)
                {
                    StartCoroutine("DoubleTap_co");
                }
            }
            else
            {
                StopCoroutine("DoubleTap_co");
                _playerAnimator.SetBool("DoubleTap", false);
                _isShooting = false;
                _isRight = false;
            }
        }
        if (_playerInput.Mouse1Up)
        {
            StopCoroutine("DoubleTap_co");
            _playerAnimator.SetBool("DoubleTap", false);
            _isShooting = false;
            _isRight = false;
        }
    }

    private void CheckPhaseRound()
    {
        if (_playerInput.Mouse2Down && _phaseRoundCooldownRemain <= 0f)
        {
            _attackCoroutine ??= StartCoroutine(PhaseRound_co());
            _phaseRoundCooldownRemain = _phaseRoundCooldown;
        }
    }

    private void CheckTacticalDive()
    {
        if (_playerInput.Shift)
        {
            _tacticalDiveCooldownRemain = _tacticalDiveCooldown;
        }
    }
    private void CheckSuppressiveFire()
    {
        if(_playerInput.Special)
        {
            _suppressiveFireCooldownRemain = _suppressiveFireCooldown;
        }
    }
    private IEnumerator DoubleTap_co()
    {
        _playerAnimator.SetBool("DoubleTap", true);
        _isShooting = true;
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
        _isShooting = false;
    }

    private IEnumerator PhaseRound_co()
    {
        _playerAnimator.SetTrigger("PhaseRound");
        while (_playerAnimator.GetCurrentAnimatorStateInfo(1).normalizedTime <= 0.999f)
        {
            yield return null;
        }
        _attackCoroutine = null;
    }

    private void CheckCooldown(ref float skillCooldownRemain)
    {
        if (skillCooldownRemain > 0)
        {
            skillCooldownRemain -= Time.deltaTime;
        }
        else
        {
            skillCooldownRemain = 0f;
        }
    }

    private void CheckAllCooldown()
    {
        CheckCooldown(ref _phaseRoundCooldownRemain);
        CheckCooldown(ref _tacticalDiveCooldownRemain);
        CheckCooldown(ref _suppressiveFireCooldownRemain);
    }

}