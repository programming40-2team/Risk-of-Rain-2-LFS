using System.Collections;
using UnityEngine;

public class BeetleQueenControl : MonoBehaviour
{
    private BeetleQueen _beetleQueen;
    private Animator _beetleQueenAnimator;

    private Transform _player;

    private float[] _skillCoolDownArr = new float[3]; // 10 15 20
    private bool[] _isSkillRun = new bool[3];
    public bool IsAniRun = false;

    private void Awake()
    {
        _beetleQueen = FindObjectOfType<BeetleQueen>();
        TryGetComponent(out _beetleQueenAnimator);
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _skillCoolDownArr[0] = 5f;
        _skillCoolDownArr[1] = 10f;
        _skillCoolDownArr[2] = 15f;

        for (int i = 0; i < _isSkillRun.Length; i++)
        {
            _isSkillRun[i] = false;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(Attack_co());
    }

    private IEnumerator Attack_co()
    {
        while (!_beetleQueen.IsDeath)
        {
            if (_beetleQueenAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !IsAniRun)
            {
                IsAniRun = true;
                if (IsPlayerInFieldOfView() && !IsPlayerBehindBoss())
                {
                    if (!_isSkillRun[0])
                    {
                        UseSkill(0);
                        Debug.Log("0번 스킬 사용 / 플레이어 시야 안에 있음");
                    }
                    else
                    {
                        _beetleQueenAnimator.SetTrigger("Aiming");
                        Debug.Log("아무것도 안 하기 / 플레이어 시야 안에 있음");
                    }
                }
                else if (!IsPlayerInFieldOfView() && IsPlayerBehindBoss())
                {
                    if (!_isSkillRun[1])
                    {
                        UseSkill(1);
                        Debug.Log("1번 스킬 사용 / 플레이어 뒤에 있음");
                    }
                    else
                    {
                        _beetleQueenAnimator.SetTrigger("Aiming");
                        Debug.Log("아무것도 안 하기 / 플레이어 시야 안에 있음");
                    }
                }
                else if (!IsPlayerInFieldOfView() && !IsPlayerBehindBoss())
                {
                    if (!_isSkillRun[2])
                    {
                        UseSkill(2);
                        Debug.Log("2번 스킬 사용");
                    }
                    else
                    {
                        float angle = CalculateAngle();
                        if (angle < 0)
                        {
                            _beetleQueenAnimator.SetTrigger("Left90");
                        }
                        else
                        {
                            _beetleQueenAnimator.SetTrigger("Right90");
                        }
                    }
                }
            }
            yield return null;
        }
    }

    private bool IsPlayerInFieldOfView()
    {
        float angle = CalculateAngle();
        if (angle >= -45 && angle < 45)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsPlayerBehindBoss()
    {
        float angle = CalculateAngle();
        if (angle < -135 || angle >= 135)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private float CalculateAngle()
    {
        Vector3 vBase = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        Vector3 vAnother = new Vector3(_player.transform.position.x - transform.position.x, 0, _player.transform.position.z - transform.position.z).normalized;

        float dot = Vector3.Dot(vBase, vAnother);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        Vector3 cross = Vector3.Cross(vBase, vAnother);
        if (cross.y < 0)
        {
            angle = -angle;
        }
        return angle;
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
        Debug.Log(skillIndex + "번 스킬 쿨 돌았음");
    }
}
