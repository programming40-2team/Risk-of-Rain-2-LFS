using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardState : MonoBehaviour, IMonsterBehavior
{
    public void MoveToward()
    {
        Debug.Log("이미 이동중");
    }

    public void Idle()
    {
        Debug.Log("이동하다가 멈추기");
    }

    public void Paralysis()
    {
        Debug.Log("이동하다가 마비걸리기");
    }

    public void Aiming()
    {
        Debug.Log("이동하다가 플레이어 노려보기");
    }

    public void SkillA()
    {
        Debug.Log("이동하다가 스킬A 사용하기");
    }

    public void SkillB()
    {
        Debug.Log("이동하다가 스킬B 사용하기");
    }

    public void SkillC()
    {
        Debug.Log("이동하다가 스킬C 사용하기");
    }

}
