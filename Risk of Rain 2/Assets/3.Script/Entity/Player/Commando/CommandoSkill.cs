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
    private Rigidbody _playerRigidbody;
    private Transform _cameraTransform;

    [SerializeField] private GameObject _leftMuzzle;
    [SerializeField] private GameObject _rightMuzzle;
    [SerializeField] private GameObject _centerMuzzle;
    private Coroutine _attackCoroutine;

    //aiming
    private float _aimY;
    private readonly float _centerAimY = 124.75f;
    private RaycastHit _aimHit;

    /// 아래는 스킬 관련 변수들입니다. 기존 쿨타임은 프로퍼티화를 시켰으나
    /// 남은 쿨타임은 ref참조가 필요하기에 Get"스킬이름"Remain 함수를 만들었습니다.

    //DoubleTap (1번째 스킬 / 평타 / 노쿨)
    private ObjectPool _bulletObjectPool;
    private bool _isRight;
    private bool _isCanShooting;
    private readonly WaitForSeconds _doubleTapDelay = new WaitForSeconds(0.167f);
    [SerializeField] private GameObject _leftMuzzleEffect;
    [SerializeField] private GameObject _rightMuzzleEffect;
    
    //PhaseRound (2번째 스킬)
    public float PhaseRoundCooldown { get; private set; } = 3f;
    private float _phaseRoundCooldownRemain  = 0f;
    public float GetPhaseRoundCooldownRemain()
    {
        return _phaseRoundCooldownRemain;
    }
    private readonly WaitForSeconds _phaseRoundDelay = new WaitForSeconds(0.1f);
    private ObjectPool _phaseRoundObjectPool;

    //Tactical Dive (3번째 스킬)
    public float TacticalDiveCooldown { get; private set; } = 4f;
    private float _tacticalDiveCooldownRemain = 0f;
    public float GetTacticalDiveCooldownRemain()
    {
        return _tacticalDiveCooldownRemain;
    }
    private readonly float _diveForce = 7f;
    private readonly WaitForSeconds _taticalDiveDelay = new WaitForSeconds(0.5f);

    //Suppressive Fire (4번째 스킬)
    public float SuppressiveFireCooldown { get; private set; } = 9f;
    private float _suppressiveFireCooldownRemain = 0f;
    public float GetSuppressiveFireCooldownRemain()
    {
        return _suppressiveFireCooldownRemain;
    }
    private readonly WaitForSeconds _suppressiveFireInterval = new WaitForSeconds(0.16667f);
    private ObjectPool _spFirePool;

    private void Awake()
    {
        TryGetComponent(out _playerInput);
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerRigidbody);
        
        _bulletObjectPool = GameObject.Find("BulletPool").GetComponent<ObjectPool>();
        _phaseRoundObjectPool = GameObject.Find("PhaseRoundPool").GetComponent<ObjectPool>();
        _spFirePool = GameObject.Find("SpFirePool").GetComponent<ObjectPool>();
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
                if (_isCanShooting)
                {
                    StartCoroutine(nameof(DoubleTap_co));
                }
            }
            else
            {
                StopCoroutine(nameof(DoubleTap_co));
                _playerAnimator.SetBool("DoubleTap", false);
                _isCanShooting = true;
                _isRight = false;
            }
        }
        if (_playerInput.Mouse1Up)
        {
            StopCoroutine(nameof(DoubleTap_co));
            _playerAnimator.SetBool("DoubleTap", false);
            _isCanShooting = true;
            _isRight = false;
        }
    }

    private void CheckPhaseRound()
    {
        if (_playerInput.Mouse2Down && _phaseRoundCooldownRemain <= 0f)
        {
            _attackCoroutine ??= StartCoroutine(PhaseRound_co());
        }
    }

    private void CheckTacticalDive()
    {
        if (_playerInput.Shift && _tacticalDiveCooldownRemain <= 0f)
        {
            _attackCoroutine ??= StartCoroutine(TacticalDive_co());
            
        }
    }
    private void CheckSuppressiveFire()
    {
        if (_playerInput.Special && _suppressiveFireCooldownRemain <= 0f)
        {
            _attackCoroutine ??= StartCoroutine(SuppressiveFire_co());
            
            
        }
    }
    private IEnumerator DoubleTap_co()
    {
        _playerAnimator.SetBool("DoubleTap", true);
        _isCanShooting = false;
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
        bullet.GetComponent<Projectile>().ShootForward();
        _isRight = !_isRight;
        yield return _doubleTapDelay;
        _isCanShooting = true;
    }

    private IEnumerator PhaseRound_co()
    {
        _phaseRoundCooldownRemain = PhaseRoundCooldown;
        _playerAnimator.SetTrigger("PhaseRound");
        GameObject PhaseRound = _phaseRoundObjectPool.GetObject();
        Vector3 bulletDirection;
        bulletDirection = _aimHit.point - _centerMuzzle.transform.position;
        PhaseRound.transform.SetPositionAndRotation(_centerMuzzle.transform.position, Quaternion.LookRotation(bulletDirection, Vector3.up));
        PhaseRound.GetComponent<Projectile>().ShootForward();
        yield return _phaseRoundDelay;
        _attackCoroutine = null;
    }

    private IEnumerator TacticalDive_co()
    {
        _tacticalDiveCooldownRemain = TacticalDiveCooldown;
        _playerAnimator.SetFloat("RollHorizon", _playerInput.HorizontalDirectionRaw);
        _playerAnimator.SetFloat("RollVertical", _playerInput.MoveRaw);
        _playerAnimator.SetTrigger("Roll");
        Vector3 direction = (transform.forward * _playerInput.MoveRaw + transform.right * _playerInput.HorizontalDirectionRaw).normalized;
        Vector3 force = direction * _diveForce;
        _playerRigidbody.AddForce(force, ForceMode.Impulse);
        yield return _taticalDiveDelay;
        _playerRigidbody.velocity = Vector3.zero;
        _attackCoroutine = null;
    }

    private IEnumerator SuppressiveFire_co()
    {
        _suppressiveFireCooldownRemain = SuppressiveFireCooldown;
        for (int i = 0; i < 6; i++)
        {
            _playerAnimator.SetTrigger("SuppressiveFire");
            _rightMuzzleEffect.SetActive(true);
            _spFirePool.GetObject().transform.position = _aimHit.point;
            yield return _suppressiveFireInterval;
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