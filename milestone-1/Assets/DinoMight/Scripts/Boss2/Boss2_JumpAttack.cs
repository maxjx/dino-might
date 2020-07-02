using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_JumpAttack : StateMachineBehaviour
{
    Vector2 prevColliderSize = new Vector2(0.21f, 0.32f);
    Vector2 prevColliderOffset = Vector2.zero;
    Vector2 newColliderSize = new Vector2(0.21f, 0.01f);
    Vector2 newColliderOffset = new Vector2(0, -0.15f);
    Vector3 endPositionOffset = new Vector3(5.52f, 0, 0);
    private BoxCollider2D collider;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        collider = animator.GetComponent<BoxCollider2D>();
        collider.size = newColliderSize;
        collider.offset = newColliderOffset;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        collider.size = prevColliderSize;
        collider.offset = prevColliderOffset;
        animator.transform.position += endPositionOffset;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
