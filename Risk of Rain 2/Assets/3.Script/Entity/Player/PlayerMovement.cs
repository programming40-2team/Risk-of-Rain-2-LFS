using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class PlayerMovement : MonoBehaviour
{
    //캐릭터가 카메라방향을 바라보기 위함
    private Transform _cameraTransform;

    //플레이어 컴포넌트
    private Animator _playerAnimator;
    private Rigidbody _playerRigidbody;
    private PlayerInput _playerInput;
    private PlayerStatus _playerStatus;
    private CapsuleCollider _playerCollider;
    private float _colliderRadius;

    //플레이어 스테이터스
    private readonly WaitForSeconds _jumpCheckTime = new WaitForSeconds(0.02f);
    private readonly float _massCoefficient = 150f;
    private float _jumpForce = 3f;
    private int _jumpCount;
    private bool _isJumping;
    private readonly float _rotateSpeed = 550f;
    private bool _isSprinting;

    //Ground Check
    private readonly float _groundCheckDistance = 0.11f;
    private readonly float _yOffset = 0.1f;

    public bool IsJumping
    {
        get { return _isJumping; }
        set
        {
            _isJumping = value;
            if (IsJumping == true)
            {
                // Managers.ItemApply.ApplyPassiveSkill(1016);
            }

        }
    }
    public int JumpCount
    {
        get { return _jumpCount; }
        set
        {
            int prevjumpcount = _jumpCount;
            _jumpCount = value;

            if (prevjumpcount != _jumpCount)
            {
                Managers.ItemApply.ApplyPassiveSkill(1016);
            }

        }
    }

    private void Awake()
    {
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerRigidbody);
        TryGetComponent(out _playerInput);
        TryGetComponent(out _playerStatus);
        TryGetComponent(out _playerCollider);
    }
    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _jumpForce *= _massCoefficient / _playerStatus.Mass;
        _jumpCount = _playerStatus.MaxJumpCount;
        _colliderRadius = _playerCollider.radius;
    }

    private void Update()
    {
        CheckGround();
        CheckSprint();
        Rotate();

        if (_playerInput.Jump)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        Vector3 _moveDirection;
        Vector3 _distance;
        Vector2 _move = new Vector2(_playerInput.HorizontalDirection, _playerInput.Move);
        _moveDirection = _move.x * transform.right + _move.y * transform.forward;
        if (_isSprinting)
        {
            _distance = 1.5f * _playerStatus.MoveSpeed * Time.deltaTime * _moveDirection.normalized;
            _playerAnimator.SetFloat("Move", 1.5f * _playerInput.Move);
        }
        else
        {
            _distance = _playerStatus.MoveSpeed * Time.deltaTime * _moveDirection.normalized;
            _playerAnimator.SetFloat("Move", _playerInput.Move);
        }
        _playerRigidbody.MovePosition(_playerRigidbody.position + _distance);
        _playerAnimator.SetFloat("Horizon", _playerInput.HorizontalDirection);
    }

    private void Rotate()
    {
        Quaternion _targetRotaition = Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotaition, _rotateSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (_jumpCount > 0)
        {
            StartCoroutine(CheckJump_co());
            if (_isJumping)
            {
                _playerRigidbody.velocity = Vector3.zero;
            }
            _playerAnimator.SetBool("Jump", true);
            _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _jumpCount--;
        }
    }

    private IEnumerator CheckJump_co()
    {
        yield return _jumpCheckTime;
        IsJumping = true;
    }

    private void CheckGround()
    {
        LayerMask layerMask = (-1) - (1 << (int)Define.LayerMask.Skill);
        if ((Physics.Raycast(transform.position + new Vector3(_colliderRadius, _yOffset, _colliderRadius), Vector3.down, out _, _groundCheckDistance, layerMask) ||
            Physics.Raycast(transform.position + new Vector3(-_colliderRadius, _yOffset, -_colliderRadius), Vector3.down, out _, _groundCheckDistance, layerMask) ||
            Physics.Raycast(transform.position + new Vector3(_colliderRadius, _yOffset, -_colliderRadius), Vector3.down, out _, _groundCheckDistance, layerMask) ||
            Physics.Raycast(transform.position + new Vector3(-_colliderRadius, _yOffset, _colliderRadius), Vector3.down, out _, _groundCheckDistance, layerMask)) && IsJumping)
        {
            _playerAnimator.SetBool("Jump", false);
            JumpCount = _playerStatus.MaxJumpCount;
            IsJumping = false;
        }
    }

    private void CheckSprint()
    {
        if (_playerInput.Sprint && _playerInput.Move > 0)
        {
            _isSprinting = !_isSprinting;
        }

        if (_playerInput.Move <= 0 || _playerInput.Mouse1Down || _playerInput.Mouse2Down || _playerInput.Special)
        {
            _isSprinting = false;
        }
    }
}