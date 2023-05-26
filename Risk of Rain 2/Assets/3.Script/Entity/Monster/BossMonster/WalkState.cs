using UnityEngine;

public class WalkState : StateMachineBehaviour
{
    private Transform _beetleQueenTransForm;
    private BeetleQueen _beetleQueen;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _beetleQueenTransForm = animator.GetComponent<Transform>();
        _beetleQueen = animator.GetComponent<BeetleQueen>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _beetleQueenTransForm.position += _beetleQueen.MoveSpeed * Time.deltaTime * _beetleQueenTransForm.forward;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
