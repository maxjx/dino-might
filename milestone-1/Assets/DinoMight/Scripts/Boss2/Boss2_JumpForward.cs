﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_JumpForward : StateMachineBehaviour
{
    Vector3 endPositionOffset = new Vector3(4.62f, 0, 0);

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    // }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform tf = animator.transform;

        // teleports boss to the position that the animation ends
        if (tf.rotation.y < 0)
        {
            tf.position = tf.position - endPositionOffset;
        }
        else
        {
            tf.position = tf.position + endPositionOffset;
        }
    }
}
