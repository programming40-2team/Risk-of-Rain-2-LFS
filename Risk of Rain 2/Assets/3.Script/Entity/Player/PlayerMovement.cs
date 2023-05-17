using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CinemachineFreeLook _virtualCamera;
    //플레이어 컴포넌트
    private Animator _playerAnimator;
    private Rigidbody _playerRigidbody;
    private PlayerInput _playerInput;
    private PlayerStatus _playerStatus;

    Vector3 dir;
    //플레이어 스테이터스
    [SerializeField] private float _jumpForce = 2f;
    private void Awake()
    {
        _virtualCamera = FindObjectOfType<CinemachineFreeLook>();
        if (_virtualCamera != null)
        {
            Debug.Log("성공");
        }
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerRigidbody);
        TryGetComponent(out _playerInput);
        TryGetComponent(out _playerStatus);
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        _playerAnimator.SetFloat("Move", _playerInput.Move);
    }
    private void Move()
    {
        Vector3 _moveDirection = new Vector3(_playerInput.HorizontalDirection, 0, _playerInput.Move);
        //Vector3 _distance = _playerInput.Move * transform.forward * Time.deltaTime * _playerStatus.MoveSpeed;
        Vector3 _distance = _moveDirection * Time.deltaTime * _playerStatus.MoveSpeed;
        _playerRigidbody.MovePosition(_playerRigidbody.position + _distance);
    }

    private void Rotate()
    {
        dir = transform.position - _virtualCamera.transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
    }

    /// <summary>
    /// 플레이어 점프 메서드
    /// </summary>
    public void Jump()
    {
        _playerRigidbody.AddForce(Vector3.up * _jumpForce);
        _playerAnimator.SetBool("Jump", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _playerAnimator.SetBool("Jump", false);
        }
    }
}
