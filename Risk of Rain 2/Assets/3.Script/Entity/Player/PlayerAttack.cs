using Cinemachine;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _playerAnimator;
    private PlayerInput _playerInput;
    private Rigidbody _playerRigidbody;

    private WaitForSeconds _attackSavingTime = new WaitForSeconds(0.32f);
    public Coroutine RunningCoroutine;
    public bool _isAttacking;
    private int _attackCount = 0;

    //유틸리티 스킬 관련
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashSpeed;
    private CinemachineFreeLook _virtualCamera;
    private Transform _cameraTransform;
    private void Awake()
    {
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerRigidbody);
        TryGetComponent(out _playerInput);
        _virtualCamera = FindObjectOfType<CinemachineFreeLook>();
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        Debug.Log(_playerInput.Mouse1);
        if (_playerInput.Mouse1 && _attackCount <= 2 && !_isAttacking)
        {
            if (_attackCount == 0)
            {
                Attack1();
            }
            else if (_attackCount == 1)
            {
                Attack2();
            }
            else if (_attackCount == 2)
            {
                Attack3();
            }
        }
        if (_playerInput.Shift)
        {
            StartCoroutine(Dash_co());
        }
    }
    public void Attack1()
    {
        _isAttacking = true;
        _playerAnimator.SetBool("Attack1", true);
        _attackCount++;
    }
    public void Attack2()
    {
        _isAttacking = true;
        _playerAnimator.SetBool("Attack2", true);
        _attackCount++;
        if (RunningCoroutine != null)
        {
            StopCoroutine(RunningCoroutine);
        }
    }
    public void Attack3()
    {
        _isAttacking = true;
        _playerAnimator.SetBool("Attack3", true);
        _attackCount++;
        if (RunningCoroutine != null)
        {
            StopCoroutine(RunningCoroutine);
        }

    }
    public IEnumerator AttackTimeCheck_co()
    {
        yield return _attackSavingTime;
        _playerAnimator.SetBool("Attack3", false);
        _playerAnimator.SetBool("Attack2", false);
        _playerAnimator.SetBool("Attack1", false);
        _attackCount = 0;
    }

    private void AnimationEnd()
    {
        _isAttacking = false;
        RunningCoroutine = StartCoroutine(AttackTimeCheck_co());
    }
    private void EndAttack()
    {
        _playerAnimator.SetBool("Attack3", false);
        _playerAnimator.SetBool("Attack2", false);
        _playerAnimator.SetBool("Attack1", false);
        _attackCount = 0;
        _isAttacking = false;
    }

    /// <summary>
    /// 대쉬 거리는 고정일 거고, 무적판정넣고, 경로에 존재하는 몬스터에게 마비와 데미지,
    /// 적을 맞히면 한 번 더 사용가능 최대 3회
    /// prep - loop - exit
    /// 무적판정과, 몬스터에게 데미지
    /// --플레이어를 트리거로 바꾼다. ontriggerEnter - 마비(나중예), 데미지
    /// </summary>
    /// <returns></returns>

    private IEnumerator Dash_co()
    {
        //Vector3 _destPos = transform.position + (transform.position - _virtualCamera.transform.position) * _dashDistance;
        Vector3 _destPos = transform.position + _cameraTransform.forward * _dashDistance;
        while (Vector3.SqrMagnitude(transform.position - _destPos) >= 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _destPos, _dashSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = _destPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<Entity>().OnDamage(10);
        }
    }
}