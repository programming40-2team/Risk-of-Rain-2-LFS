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
    private bool _isRotate = false;

    private enum RotationAngle { LEFT45, LEFT90, LEFT135, RIGHT45, RIGHT90, RIGHT135}
    public enum BossState { IDLE, WALK, ROTATING, USINGSKILL}

    private BossState _currentState = BossState.IDLE;

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

    private void OnEnable()
    {
        StartCoroutine(UpdateState_co());
    }

    private IEnumerator UpdateState_co()
    {
        while(!_beetleQueen.IsDeath)
        {
            Debug.Log("BeetleQueen current state : " + _currentState);
            switch (_currentState)
            {
                case BossState.IDLE:
                    if (!_isSkillRun[0] && IsPlayerInFieldOfView())
                    {
                        ChangeState(BossState.USINGSKILL);
                        UseSkill(0); //스킬1 발동 SetTrigger("FireSpit");
                        Debug.Log("1");
                    }
                    else if (!_isSkillRun[1] && IsPlayerBehindBoss())
                    {
                        ChangeState(BossState.USINGSKILL);
                        UseSkill(1); //스킬2발동 SetTrigger("SpawnWard");
                        Debug.Log("2");
                    }
                    else if (!_isSkillRun[0] && !IsPlayerInFieldOfView())
                    {
                        Debug.Log("3");
                        //ChangeState(BossState.ROTATING);
                    }
                    else if (!_isSkillRun[1] && !IsPlayerBehindBoss())
                    {
                        //ChangeState(BossState.ROTATING);
                        Debug.Log("4");
                    }
                    else if (!_isSkillRun[2])
                    {
                        ChangeState(BossState.USINGSKILL);
                        UseSkill(2); //스킬3발동 SetTrgger("RangeBomb");
                        Debug.Log("5");
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
                    if(!_isRotate)
                    {
                        _isRotate = true;
                        _beetleQueenAnimator.SetTrigger("Aiming");
                        yield return null;
                        if (_beetleQueenAnimator.GetCurrentAnimatorStateInfo(0).IsName("BeetleQueenArmature|aimHorizontal"))
                        {
                            if (_beetleQueenAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.97)
                            {
                                float angle = CalculateAngle(transform.forward, _player.transform.position - transform.position);
                                if (angle >= -75 && angle < -45)
                                {
                                    _beetleQueenAnimator.SetTrigger("Left45");
                                }
                                else if (angle >= -105 && angle < -75)
                                {
                                    _beetleQueenAnimator.SetTrigger("Left90");
                                }
                                else if (angle >= -135 && angle < -105)
                                {
                                    _beetleQueenAnimator.SetTrigger("Left135");
                                }
                                else if (angle >= 45 && angle < 75)
                                {
                                    _beetleQueenAnimator.SetTrigger("Right45");
                                }
                                else if (angle >= 75 && angle < 105)
                                {
                                    _beetleQueenAnimator.SetTrigger("Right90");
                                }
                                else if (angle >= 105 && angle < 135)
                                {
                                    _beetleQueenAnimator.SetTrigger("Right135");
                                }
                                //ChangeState(BossState.IDLE);
                                //_isRotate = false;
                            }
                        }
                    }
                    break;
                case BossState.USINGSKILL:
                    yield return null;
                    if (!_beetleQueen.IsRun) // 스킬 시전 끝나면 바꿈 (쿨타임 아님 주의)
                    {
                        ChangeState(BossState.IDLE);
                    }
                    break;
            }
            yield return null;
        }
    }
    // 회전하는 애니메이션 끝날때 상태Idle로 바꾸고 bool 변수 바꾸기
    // Walk도 쿨타임 해야할듯..? / 안하니까 계속 계속 걸어가..



    private bool IsPlayerInFieldOfView() // 플레이어가 보스 시야에 있는지 없는지 판단하는 메소드
    {
        float angle = CalculateAngle(transform.forward, _player.transform.position - transform.position);
        if(angle >= -45 && angle < 45)
        {
            Debug.Log("플레이어가 보스 시야에 있습니다.");
            return true;
        }
        else
        {
            Debug.Log("플레이어가 보스 시야에 없습니다.");
            return false;
        }
    }

    private bool IsPlayerBehindBoss() // 플레이어가 보스 뒤쪽에 있는지 없는지 판단하는 메소드 / 수정해야함
    {
        float angle = CalculateAngle(transform.forward, _player.transform.position - transform.position);
        if (angle < -135 || angle >= 135)
        {
            Debug.Log("플레이어가 보스 뒤에 있습니다.");
            return true;
        }
        else
        {
            Debug.Log("플레이어가 보스 뒤에 없습니다.");
            return false;
        }
    }

    private void ChangeState(BossState newState)
    {
        _currentState = newState;
    }

    private float CalculateAngle(Vector3 vStart, Vector3 vEnd)
    {
        // 각도 계산
        Vector3 v = vEnd - vStart;

        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    private void MoveTowardsPlayer() // 보스가 플레이어 향해 걷는 메소드
    {
        _beetleQueenAnimator.SetTrigger("Walk"); // 이동은 애니메이터에 이벤트로 있음
        if(_beetleQueenAnimator.GetCurrentAnimatorStateInfo(0).IsName("BeetleQueenArmature|walkForward"))
        {
            if(_beetleQueenAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99)
            {
                ChangeState(BossState.IDLE);
            }
        }
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
