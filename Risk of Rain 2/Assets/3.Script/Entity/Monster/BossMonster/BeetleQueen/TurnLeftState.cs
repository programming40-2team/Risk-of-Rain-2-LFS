using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLeftState : StateMachineBehaviour
{
    private BeetleQueenControl _beetleQueenControl;
    private BeetleQueen _beetleQueen;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _beetleQueenControl = animator.GetComponent<BeetleQueenControl>();
        _beetleQueen = animator.GetComponent<BeetleQueen>();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Rotate(-90);
        _beetleQueenControl.IsAniRun = false;
    }
    public void Rotate(float angle)
    {
        _beetleQueen.transform.Rotate(new Vector3(0, angle, 0));
    }
}
