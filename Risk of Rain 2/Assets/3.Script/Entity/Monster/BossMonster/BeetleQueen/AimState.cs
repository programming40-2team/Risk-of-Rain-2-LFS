using UnityEngine;

public class AimState : StateMachineBehaviour
{
    private BeetleQueenControl _beetleQueenControl;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _beetleQueenControl = animator.GetComponent<BeetleQueenControl>();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _beetleQueenControl.IsAniRun = false;
    }
}
