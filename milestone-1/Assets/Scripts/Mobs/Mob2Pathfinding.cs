using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Mob2Pathfinding : MonoBehaviour
{
    public Transform target;

    public float speed = 300f;
    public float nextWaypointDistance = 3f;

    //public Transform mobGFX;

    Path path;
    int currentWaypoint = 0;
    //bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

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

        if (currentWaypoint >= path.vectorPath.Count)
        {
            //reachedEndOfPath = true;
            return;
        }
        // else
        // {
        //     reachedEndOfPath = false;
        // }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        float sqdistance = (rb.position - (Vector2)path.vectorPath[currentWaypoint]).sqrMagnitude;

        if (sqdistance < nextWaypointDistance * nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            transform.localScale = new Vector2(-4f, 4f);
        }
        else if (force.x <= -0.01f)
        {
            transform.localScale = new Vector2(4f, 4f);
        }
    }
}
