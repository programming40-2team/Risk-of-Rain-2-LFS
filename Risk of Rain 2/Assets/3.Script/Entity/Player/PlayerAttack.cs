using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _playerAnimator;

    private WaitForSeconds _attackSavingTime = new WaitForSeconds(0.32f);
    public Coroutine RunningCoroutine;
    public bool IsAttacking;
    public int AttackCount = 1;

    private void Awake()
    {
        TryGetComponent(out _playerAnimator);
    }

    public void Attack1()
    {
        IsAttacking = true;
        _playerAnimator.SetBool("Attack1", true);
        AttackCount++;
    }
    public void Attack2() 
    {
        IsAttacking = true;
        _playerAnimator.SetBool("Attack2", true);
        AttackCount++;
        StopCoroutine(RunningCoroutine);
    }
    public void Attack3()
    {
        IsAttacking = true;
        _playerAnimator.SetBool("Attack3", true);
        AttackCount++;
        StopCoroutine(RunningCoroutine);
        
    }
    public IEnumerator AttackTimeCheck_co()
    {
        yield return _attackSavingTime;
        _playerAnimator.SetBool("Attack3", false);
        _playerAnimator.SetBool("Attack2", false);
        _playerAnimator.SetBool("Attack1", false);
        AttackCount = 0;
    }

    private void AnimationEnd()
    {
        IsAttacking = false;
        RunningCoroutine = StartCoroutine(AttackTimeCheck_co());
       
    }
    private void EndAttack()
    {
        _playerAnimator.SetBool("Attack3", false);
        _playerAnimator.SetBool("Attack2", false);
        _playerAnimator.SetBool("Attack1", false);
        AttackCount = 0;
        IsAttacking = false;
    }
}
