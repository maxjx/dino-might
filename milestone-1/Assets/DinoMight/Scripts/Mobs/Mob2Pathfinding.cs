using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Mob2Pathfinding : MonoBehaviour
{
    public Transform target;

    public float speed = 300f;
    public float nextWaypointDistance = 3f;
    public float distanceToEngageTarget = 10f;
    public bool engageTarget = false;

    private Path path;
    private int currentWaypoint = 0;
    private bool facingRight;
    // private float idleDuration = 3f;    // Time taken for eagle to move to another random spot
    // private float timer;                // For idling around
    // private int count;                  // Count number of times it moved to a random idle spot
    private Vector2 idletarget;
    private Vector2 origPos;

    private Seeker seeker;
    private Rigidbody2D rb;
    private MobHealth health;
    private Animator animator;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<MobHealth>();
        animator = GetComponent<Animator>();
        origPos = transform.position;
        idletarget = transform.position;

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        // If the path to the target is reached, or
        // if distance from target is more than distance to engage, 
        if (path == null
            || currentWaypoint >= path.vectorPath.Count
            || (transform.position - target.position).sqrMagnitude >= distanceToEngageTarget * distanceToEngageTarget)
        {
            // if (count == 4)
            // {
            //     GoBackToOrigPos();
            //     count = 0;
            // }
            // else if (timer <= 0f)
            // {
            //     if (((Vector2)transform.position - idletarget).sqrMagnitude < 0.05f)
            //     {
            //         idletarget = Vector2.MoveTowards(transform.position, (Random.insideUnitCircle * 7) + (Vector2)transform.position, 0.02f);
            //     }
            //     else
            //     {
            //         rb.MovePosition(idletarget);
            //     }
            //     count++;
            //     timer = idleDuration;
            // }
            // else
            // {
            //     timer -= Time.deltaTime;
            // }
            //animator.SetBool("idle", true);
            return;
        }
        else if (engageTarget || health.WasHurt())
        {
            //animator.SetBool("idle", false);
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
            rb.AddForce(force);

            float sqdistance = (rb.position - (Vector2)path.vectorPath[currentWaypoint]).sqrMagnitude;

            if (sqdistance < nextWaypointDistance * nextWaypointDistance)
            {
                currentWaypoint++;
            }

            if (force.x >= 0.01f && !facingRight)
            {
                Flip();
            }
            else if (force.x <= -0.01f && facingRight)
            {
                Flip();
            }
        }
    }

    public void IdleAround()
    {
        // for (int i = 0; i < 7; i++)
        // {
        //     transform.position = Vector2.MoveTowards(transform.position, (Random.insideUnitCircle * 7) + (Vector2)transform.position, 0.1f);

        // }
        rb.AddForce((Random.insideUnitCircle * 7) * speed * Time.deltaTime);
    }

    // Go back to original position, called by idle state after idling awhile
    public void GoBackToOrigPos()
    {
        rb.AddForce((origPos - rb.position) * speed * Time.deltaTime);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void EngageTarget()
    {
        engageTarget = true;
    }
}