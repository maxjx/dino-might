using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_Summon : StateMachineBehaviour
{
    Transform tf;
    Vector3 offset_x;
    Vector3 lightningOffset_y;
    Vector3 cloudOffset_y;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        tf = animator.transform;
        offset_x = new Vector3(3.8f, 0, 0);
        if (tf.rotation.y < 0)
        {
            offset_x.x = -offset_x.x;
        }
        cloudOffset_y = new Vector3(0, 3.5f, 0);
        ObjectPooler.Instance.SpawnFromPool("Cloud", tf.position + offset_x + cloudOffset_y, tf.rotation);
        ObjectPooler.Instance.SpawnFromPool("Cloud", tf.position + (offset_x*2) + cloudOffset_y, tf.rotation);
        ObjectPooler.Instance.SpawnFromPool("Cloud", tf.position + (offset_x*3) + cloudOffset_y, tf.rotation);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        lightningOffset_y = new Vector3(0, 1.2f, 0);
        ObjectPooler.Instance.SpawnFromPool("Lightning", tf.position + offset_x + lightningOffset_y, tf.rotation);
        ObjectPooler.Instance.SpawnFromPool("Lightning", tf.position + (offset_x*2) + lightningOffset_y, tf.rotation);
        ObjectPooler.Instance.SpawnFromPool("Lightning", tf.position + (offset_x*3) + lightningOffset_y, tf.rotation);
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
