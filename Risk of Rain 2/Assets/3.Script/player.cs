using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    private PlayerInput _playerInput;

    private float _moveSpeed = 5f;
    private float _rotateSpeed = 270;

    private void Awake()
    {
        TryGetComponent(out _playerRigidbody);
        TryGetComponent(out _playerInput);
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
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

    public void ReturnIdle()
    {
    }
}