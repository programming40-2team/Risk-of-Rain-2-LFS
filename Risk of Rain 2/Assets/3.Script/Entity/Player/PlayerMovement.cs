using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class PlayerMovement : MonoBehaviour
{
    //캐릭터가 카메라방향을 바라보기 위함
    private CinemachineFreeLook _virtualCamera;

    //플레이어 컴포넌트
    private Animator _playerAnimator;
    private Rigidbody _playerRigidbody;
    private PlayerInput _playerInput;
    private PlayerStatus _playerStatus;
    private PlayerAttack _playerAttack;
    //플레이어 스테이터스
    private float _jumpForce = 200;
    private readonly float _massCoefficient = 0.01f;
    private int _jumpCount;
    private bool _isJumping;
    private readonly WaitForSeconds _jumpCheckTime = new WaitForSeconds(0.02f);
    private float _rotateSpeed = 550f;
    [HideInInspector] public bool IsSprinting;

    //Ground Check
    private readonly float _groundCheckDistance = 0.11f;
    private readonly float _yOffset = 0.1f;

    //이동에 영향을 주는 아이템 관련
    private int _bonusJumpCount = 0;
    private float _bonusMoveSpeed = 1f;

    private void Awake()
    {
        _virtualCamera = FindObjectOfType<CinemachineFreeLook>();
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerRigidbody);
        TryGetComponent(out _playerInput);
        TryGetComponent(out _playerStatus);
        TryGetComponent(out _playerAttack);

    }
    private void Start()
    {
        _jumpForce *= _playerStatus.Mass * _massCoefficient;
        _jumpCount = _playerStatus.MaxJumpCount;
        //StartCoroutine(Rotate_co());
    }
    private void Update()
    {
        if (Physics.Raycast(transform.position + new Vector3(0, _yOffset, 0), Vector3.down, out _, _groundCheckDistance) && _isJumping)
        {
            _playerAnimator.SetBool("BonusJump", false);
            _playerAnimator.SetBool("Jump", false);
            _jumpCount = _playerStatus.MaxJumpCount + _bonusJumpCount;
            _isJumping = false;
        }
    }
    private void FixedUpdate()
    {
        Move();
        Rotate();

        if (IsSprinting)
        {
            _playerAnimator.SetFloat("Move", 1.5f * _playerInput.Move);
        }
        else
        {
            _playerAnimator.SetFloat("Move", _playerInput.Move);
        }
        _playerAnimator.SetFloat("HorizontalDirection", _playerInput.HorizontalDirection);
    }
    private void Move()
    {
        Vector3 _moveDirection;
        Vector3 _distance;
        _moveDirection = transform.right * _playerInput.HorizontalDirection + transform.forward * _playerInput.Move;

        if (IsSprinting)
        {
            _distance = 1.5f * _playerStatus.MoveSpeed * _bonusMoveSpeed * Time.deltaTime * _moveDirection.normalized;
        }
        else
        {
            _distance = _playerStatus.MoveSpeed * _bonusMoveSpeed * Time.deltaTime * _moveDirection.normalized;
        }
        _playerRigidbody.MovePosition(_playerRigidbody.position + _distance);
    }
    private void Rotate()
    {
        Quaternion _rotation;
        Vector3 _dir = transform.position - _virtualCamera.transform.position;
        _rotation = Quaternion.LookRotation(new Vector3(_dir.x, 0, _dir.z));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rotation, Time.deltaTime * _rotateSpeed);
    }
    /// <summary>
    /// 플레이어 점프 메서드
    /// </summary>
    public void Jump()
    {
        if (_jumpCount > 0)
        {
            StartCoroutine(CheckJump_co());
            _playerRigidbody.velocity = Vector3.zero;
            _playerRigidbody.AddForce(Vector3.up * _jumpForce);
            if (_jumpCount == _playerStatus.MaxJumpCount + _bonusJumpCount)
            {
                if (_playerAttack.IsAttacking)
                {
                    _playerAnimator.SetTrigger("JumpT");
                    _playerAnimator.SetBool("Jump", true);
                    _playerAttack.IsAttacking = false;
                    _playerAttack.RunningCoroutine = StartCoroutine(_playerAttack.AttackTimeCheck_co());
                }
                else
                {
                    _playerAnimator.SetBool("Jump", true);
                }
            }
            else
            {
                _playerAnimator.SetBool("BonusJump", true);
            }
            _jumpCount--;
        }
    }
    private IEnumerator CheckJump_co()
    {
        yield return _jumpCheckTime;
        _isJumping = true;
    }
}