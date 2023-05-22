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

    [SerializeField] private float _dashDistance;

    private Rigidbody _playerRigidbody;
    private void Awake()
    {
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerRigidbody);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash_co());
        }
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
        yield return null;
        Vector3 _dir = transform.forward;
        _playerRigidbody.MovePosition(transform.position + _dir * _dashDistance);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("충돌2");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<Entity>().OnDamage(10);
        }
    }
}