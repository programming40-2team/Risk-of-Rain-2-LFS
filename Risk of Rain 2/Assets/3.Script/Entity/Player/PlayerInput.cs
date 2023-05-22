using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerAttack _playerAttack;
    private string _verticalAxis = "Vertical";
    private string _HorizontalAxis = "Horizontal";

    public float Move { get; private set; }
    public float HorizontalDirection { get; private set; }

    private void Start()
    {
        Cursor.visible = false;
        TryGetComponent(out _playerMovement);
        TryGetComponent(out _playerAttack);
    }
    private void Update()
    {
        Move = Input.GetAxis(_verticalAxis);
        HorizontalDirection = Input.GetAxis(_HorizontalAxis);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerMovement.Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && Move > 0)
        {
            _playerMovement.IsSprinting = !_playerMovement.IsSprinting;
        }
        if (Move <= 0)
        {
            _playerMovement.IsSprinting = false;
        }
        if(Input.GetMouseButtonDown(0) && _playerAttack.AttackCount <= 2 && !_playerAttack.IsAttacking)
        {
            if (_playerAttack.AttackCount == 0)
            {
                _playerAttack.Attack1();
            }
            else if (_playerAttack.AttackCount == 1)
            {
                _playerAttack.Attack2();
            }
            else if (_playerAttack.AttackCount == 2)
            {
                _playerAttack.Attack3();
            }
        }
    }
}
