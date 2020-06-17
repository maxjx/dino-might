using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Mob2Pathfinding : MonoBehaviour
{
    public Transform target;

    public float speed = 300f;
    public float nextWaypointDistance = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private bool facingRight;

    private Seeker seeker;
    private Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

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
        if (path == null)
        {
            return;
        }

        // If the path to the target is reached,
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

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


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}