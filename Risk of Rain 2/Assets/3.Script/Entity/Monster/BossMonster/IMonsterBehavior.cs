using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterBehavior
{
    void MoveToward();
    void Idle();
    void Paralysis();
    void Aiming();
    void SkillA();
    void SkillB();
    void SkillC();

}
