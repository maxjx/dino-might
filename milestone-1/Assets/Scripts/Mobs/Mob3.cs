using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob3 : MonoBehaviour
{
    public float speed = 1f;
    public float roamingRange = 2f;     // Roams this distance away from the starting point and turns around

    private Animator animator;
    private Transform player;
    private Rigidbody2D rb;
    private Vector2 rightEnd;
    private Vector2 leftEnd;
    private bool movingRight = true;
    private Vector2 targetPos;          // The target position is should move towards
    private bool playerInRange = false;
    private bool shooting = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Vector2 startingpt = transform.position;    // Rigidbody2D's Y position is frozen to maintain a straight line of roaming

        leftEnd = new Vector2(startingpt.x - roamingRange, startingpt.y);
        rightEnd = new Vector2(startingpt.x + roamingRange, startingpt.y);
    }

    void FixedUpdate()
    {
        Vector2 currPos = transform.position;

        if (shooting || playerInRange)
        {
            StartCoroutine(ShootLazer());
        }
        else
        {
            if (currPos.x <= leftEnd.x)
            {
                movingRight = true;
                Flip();
            }
            else if (currPos.x >= rightEnd.x)
            {
                movingRight = false;
                Flip();
            }
            // else current position is between the 2 roaming ends and should not change direction

            // Determines its target position. Also accounts for the starting direction when currPos is btw the 2 roamEnds
            if (movingRight)
            {
                targetPos = Vector2.MoveTowards(rb.position, rightEnd, speed * Time.fixedDeltaTime);
            }
            else
            {
                targetPos = Vector2.MoveTowards(rb.position, leftEnd, speed * Time.fixedDeltaTime);
            }

            rb.MovePosition(targetPos);
        }
    }

    IEnumerator ShootLazer()
    {
        shooting = true;
        
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(1f);

        shooting = false;
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        transform.Rotate(0f, 180f, 0f);
    }
}
