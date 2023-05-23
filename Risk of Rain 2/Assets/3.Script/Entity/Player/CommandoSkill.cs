using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandoSkill : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Animator _playerAnimator;

    private void Awake()
    {
        TryGetComponent(out _playerInput);
        TryGetComponent(out _playerAnimator);
    }

    private void Update()
    {
        if (_playerInput.Mouse1)
        {
            DoubleTap();
        }
    }

    private void DoubleTap()
    {
        _playerAnimator.SetBool("DoubleTap", true);
    }
}
