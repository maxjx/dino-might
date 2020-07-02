using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_Walk : StateMachineBehaviour
{
    public float speed = 10f;
    public float movementSmoothing = 0.05f;

    private Transform player;
    private Transform ownTransform;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private Vector2 leftVelocity;           // Displacement in the left direction per step
    private Vector2 rightVelocity;
    private Vector2 currentVelocity = Vector2.zero;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<Boss2Logic>().player.transform;
        ownTransform = animator.transform;
        rb = animator.GetComponent<Rigidbody2D>();

        float step = speed * Time.fixedDeltaTime * 10f;
        leftVelocity = new Vector2(-step, 0);
        rightVelocity = new Vector2(step, 0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float displacement = player.position.x - rb.position.x;
        Vector2 targetVelocity;
        if (displacement > 0.1f)   // Player is to the right
        {
            targetVelocity = rightVelocity;
        }
        else if (displacement < -0.1f)     // Player is to the left
        {
            targetVelocity = leftVelocity;
        }
        else    // position.x is close enough to playerPos.x
        {
            targetVelocity = Vector2.zero;   // zero velocity
        }
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, movementSmoothing);

        if (displacement < -0.001f && facingRight)
        {
            Flip();
        }
        else if (displacement > 0.001f && !facingRight)
        {
            Flip();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       rb.velocity = Vector2.zero;
    }

    void Flip()
    {
        facingRight = !facingRight;
        ownTransform.Rotate(0, 180, 0);
    }
}
