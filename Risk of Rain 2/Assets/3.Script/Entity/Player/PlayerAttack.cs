using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _playerAnimator;

    private WaitForSeconds _attackSavingTime = new WaitForSeconds(2f);
    private WaitForSeconds _attackTime = new WaitForSeconds(0.4f);
    public bool IsAttacking;
    private Coroutine _runningCoroutine;
    public int AttackCount = 0;
    private void Awake()
    {
        TryGetComponent(out _playerAnimator);
    }

    public void Attack1()
    {
        StartCoroutine(AttackCheck_co());
        _runningCoroutine = StartCoroutine(AttackTimeCheck_co());
        _playerAnimator.SetBool("Attack1", true);
        AttackCount++;
    }

    public void Attack2() 
    {
        StartCoroutine(AttackCheck_co());
        StopCoroutine(_runningCoroutine);
        _runningCoroutine = StartCoroutine(AttackTimeCheck_co());
        _playerAnimator.SetBool("Attack2", true);
        AttackCount++;
    }
    public void Attack3()
    {
        StartCoroutine(AttackCheck_co());
        StopCoroutine(_runningCoroutine);
        _runningCoroutine = StartCoroutine(AttackTimeCheck_co());
        _playerAnimator.SetBool("Attack3", true);
        AttackCount++;
    }
    private IEnumerator AttackTimeCheck_co()
    {
        yield return _attackSavingTime;
        _playerAnimator.SetBool("Attack3", false);
        _playerAnimator.SetBool("Attack2", false);
        _playerAnimator.SetBool("Attack1", false);
        AttackCount = 0;
        StopCoroutine(_runningCoroutine);
    }
    private IEnumerator AttackCheck_co()
    {
        IsAttacking = true;
        yield return _attackTime;
        IsAttacking = false;
    }
}
