using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob2_idle : StateMachineBehaviour
{
    float idleDuration = 2f;    // Time taken for eagle to move to another random spot
    float timer;                // For idling around
    Mob2Pathfinding mobMovement;
    int count = 0;
    Vector2 target;
    Rigidbody2D rb;
    Transform currtransform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mobMovement = animator.GetComponent<Mob2Pathfinding>();
        rb = animator.GetComponent<Rigidbody2D>();
        currtransform = animator.GetComponent<Transform>();
        timer = idleDuration;
        count = 0;
        target = currtransform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 currPos = currtransform.position;
        if (count == 4)
        {
            mobMovement.GoBackToOrigPos();
            count = 0;
        }
        else if (timer <= 0f)
        {
            if ((currPos - target).sqrMagnitude < 0.05f)
            {
                target = Vector2.MoveTowards(currPos, (Random.insideUnitCircle * 7) + currPos, 0.02f);
            }
            else
            {
                rb.MovePosition(target);
            }
            count++;
            timer = idleDuration;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
