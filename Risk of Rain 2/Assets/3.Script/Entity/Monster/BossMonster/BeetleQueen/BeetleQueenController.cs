using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleQueenController : MonoBehaviour
{
    private BeetleQueen _beetleQueen;
    private Animator _beetleQueenAnimator;

    private Transform _player;

    // StartAcidBileSkill() 10초
    // StartWardSkill() 18초 체력 / 50% 미만일때
    // StartRangeBombSkill() 20초 체력 / 30% 미만일때
    private float[] _skillCoolDownArr = new float[3]; // 10 18 20
    private bool[] _isSkillRun = new bool[3];

    private enum RotationAngle { LEFT45, LEFT90, LEFT135, RIGHT45, RIGHT90, RIGHT135}
    private enum BossState { IDLE, WALK, ROTATING, USINGSKILL}

    private RotationAngle _currentRotationAngle;
    private BossState _currentState = BossState.IDLE;

    private bool _playerInFieldOfView = false;

    private void Awake()
    {
        _beetleQueen = FindObjectOfType<BeetleQueen>();
        TryGetComponent(out _beetleQueenAnimator);
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _skillCoolDownArr[0] = 10f;
        _skillCoolDownArr[1] = 15f;
        _skillCoolDownArr[2] = 20f;

        for (int i = 0; i < _isSkillRun.Length; i++)
        {
            _isSkillRun[i] = false;
        }
    }

    private void Update()
    {
        switch (_currentState)
        {
            case BossState.IDLE:
                if (_isSkillRun[0] && IsPlayerInFieldOfView())
                {
                    ChangeState(BossState.USINGSKILL);
                    UseSkill(0); //스킬1 발동 SetTrigger("FireSpit");
                }
                else if (_isSkillRun[0] && !IsPlayerInFieldOfView())
                {
                    ChangeState(BossState.ROTATING);
                }
                else if (_isSkillRun[1] && IsPlayerBehindBoss())
                {
                    ChangeState(BossState.USINGSKILL);
                    UseSkill(1); //스킬2발동 SetTrigger("SpawnWard");
                }
                else if (_isSkillRun[1] && !IsPlayerBehindBoss())
                {
                    ChangeState(BossState.ROTATING);
                }
                else if (_isSkillRun[2])
                {
                    ChangeState(BossState.USINGSKILL);
                    UseSkill(2); //스킬3발동 SetTrgger("RangeBomb");
                }
                else
                {
                    ChangeState(BossState.WALK); // SetTrigger("Walk");
                }
                break;
            case BossState.WALK:
                MoveTowardsPlayer(); // SetTrigger("Walk")
                break;
            case BossState.ROTATING:
                // SetTrigger("Aiming")
                break;
            case BossState.USINGSKILL:
                if (!_beetleQueen.IsRun) // 스킬 시전 끝나면 바꿈 (쿨타임 아님 주의)
                {
                    ChangeState(BossState.IDLE);
                }
                break;
        }
    }

    private bool IsPlayerInFieldOfView() // 플레이어가 보스 시야에 있는지 없는지 판단하는 메소드
    {
        return true;
    }

    private bool IsPlayerBehindBoss() // 플레이어가 보스 뒤쪽에 있는지 없는지 판단하는 메소드 / 수정해야함
    {
        return true;
    }

    private void ChangeState(BossState newState)
    {
        _currentState = newState;

        if(_currentState == BossState.ROTATING)
        {
            _beetleQueenAnimator.SetTrigger("Aiming");
            CalculateAngle(); // 각도 계산
            // 각각 회전하는 애니메이션이 끝날때 IDLE로 상태 바꿔줘야함
        }
    }

    private void CalculateAngle()
    {
        // 각도 계산 하고 얼만큼 회전 시킬건지 애니메이션 실행 / SetTrigger("Left45");
    }

    private void MoveTowardsPlayer() // 보스가 플레이어 향해 걷는 메소드
    {
        _beetleQueenAnimator.SetTrigger("Walk"); // 이동은 애니메이터에 이벤트로 있음
        // Has Exit Time 체크 -> Idle 애니메이션으로 넘어감
        // 애니메이션 끝날때 IDLE로 상태는 바꿔줘야함
    }
    private void UseSkill(int skillIndex) // 스킬 사용
    {
        StartCoroutine(UseSkill_co(skillIndex));
    }

    private IEnumerator UseSkill_co(int skillIndex)
    {
        switch (skillIndex)
        {
            case 0:
                _beetleQueenAnimator.SetTrigger("FireSpit"); // 스킬은 애니메이터에 이벤트로 있음
                _isSkillRun[skillIndex] = true;
                break;
            case 1:
                _beetleQueenAnimator.SetTrigger("SpawnWard"); // 스킬은 애니메이터에 이벤트로 있음
                _isSkillRun[skillIndex] = true;
                break;
            case 2:
                _beetleQueenAnimator.SetTrigger("RangeBomb"); // 스킬은 애니메이터에 이벤트로 있음
                _isSkillRun[skillIndex] = true;
                break;
        }
        yield return new WaitForSeconds(_skillCoolDownArr[skillIndex]); // 쿨타임만큼 기다리기
        _isSkillRun[skillIndex] = false; // 스킬 쿨타임 다 돌았음
    }
}
