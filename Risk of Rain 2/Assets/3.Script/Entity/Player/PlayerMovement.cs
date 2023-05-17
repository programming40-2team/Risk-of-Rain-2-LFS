using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //플레이어 컴포넌트
    private Animator _playerAnimator;
    private Rigidbody _playerRigidbody;
    private PlayerInput _playerInput;

    //플레이어 스테이터스
    private float _moveSpeed = 5f;
    private float _rotateSpeed = 270;
    [SerializeField]
    private float _jumpForce = 2f;
    private void Awake()
    {
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerRigidbody);
        TryGetComponent(out _playerInput);
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        _playerAnimator.SetFloat("Move", _playerInput.Move);
    }
    private void Move()
    {
        Vector3 _distance = _playerInput.Move * transform.forward * Time.deltaTime * _moveSpeed;
        _playerRigidbody.MovePosition(_playerRigidbody.position + _distance);
    }

    private void Rotate()
    {
        Quaternion _rotation = Quaternion.Euler(new Vector3(0, _playerInput.Rotate * Time.deltaTime * _rotateSpeed, 0));

        _playerRigidbody.rotation *= _rotation;
    }
    /// <summary>
    /// 플레이어 점프 메서드
    /// </summary>
    public void Jump()
    {
        _playerRigidbody.AddForce(Vector3.up * _jumpForce);
        _playerAnimator.SetBool("Jump", true);
        Debug.Log("점프");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _playerAnimator.SetBool("Jump", false);
        }
    }
}
