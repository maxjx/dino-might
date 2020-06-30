using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob1Movement : MonoBehaviour
{
    public float roamingRange = 1f;
    public float chasingRange = 3f;     // Maximum distance from spawn point at which mob starts chasing player
    public float speed = 5f;
    [Range(0, .3f)] public float movementSmoothing = 0.05f;	// How much to smooth out the movement
    public Transform groundDetector;    // Casts a ray from this point to detect ground so that it does not go off the platform

    // Coordinates of roaming boundary ends and player GameObject
    private Vector2 roamLeftEnd;
    private Vector2 roamRightEnd;
    private GameObject player;
    private Animator animator;
    private Rigidbody2D rb;      // of this mob

    private float step;     // Distance per fixedUpdate used in calculating velocity change
    private Vector2 targetVelocity;         // Displacement per fixedUpdate
    private Vector2 leftVelocity;           // Displacement in the left direction per step
    private Vector2 rightVelocity;
    private Vector2 currentVelocity = Vector2.zero;
    private bool facingRight = true;        // Rotate the sprite to face target which can be a roaming-end or player
    private bool wasRoamingRight = true;    // To remember the end that it was heading towards so that it can turn back
    private float raycastDistance = 1f;     // Distance that raycast detects for ground

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        Vector2 startingpt = transform.position;    // Rigidbody2D's Y position is frozen to maintain a straight line of roaming

        roamLeftEnd = new Vector2(startingpt.x - roamingRange, startingpt.y);
        roamRightEnd = new Vector2(startingpt.x + roamingRange, startingpt.y);

        step = speed * Time.fixedDeltaTime * 10f;
        leftVelocity = new Vector2(-step, 0);
        rightVelocity = new Vector2(step, 0);
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;  // Current position

        if (player == null)
        {
            return;     // Wont move either
        }
        Vector2 playerPos = player.transform.position;

        bool groundHit = Physics2D.Raycast(groundDetector.position, Vector2.down, raycastDistance);     // Returns true if raycast hits a collider
        float sqdistanceFromPlayer = (playerPos - position).sqrMagnitude;   // Used square instead of Vector2.Distance for optimization
        bool withinChasingRange = sqdistanceFromPlayer <= chasingRange * chasingRange;

        // If player is within chasing range and this mob is on a platform, ...
        if (withinChasingRange && groundHit)
        {
            if (position.x - playerPos.x > 0.1f)   // Player is to the left
            {
                targetVelocity = leftVelocity;
            }
            else if (position.x - playerPos.x < -0.1f)     // Player is to the right
            {
                targetVelocity = rightVelocity;
            }
            else    // position.x is close enough to playerPos.x
            {
                targetVelocity = Vector2.zero;   // zero velocity
            }
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity * 2, ref currentVelocity, movementSmoothing); // To chase faster
        }
        // within chasing range of player and mob is at the edge of the platform,
        else if (withinChasingRange && !groundHit)
        {
            // if player is to the left of mob, but facing right,
            if (playerPos.x < position.x && facingRight)
            {
                targetVelocity = leftVelocity;      // Will Flip later and continue walking
            }
            else if (playerPos.x > position.x && !facingRight)
            {
                targetVelocity = rightVelocity;
            }
            // mob is facing player but there is no ground betwee them
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        // Not chasing and should return to opposite roam end
        else if (!withinChasingRange)
        {
            // If current position is further left or at the left end of the roaming range, ...
            if (position.x <= roamLeftEnd.x)
            {
                targetVelocity = rightVelocity;     // Go to the right
                wasRoamingRight = true;
            }   // If current position is further right or at the right end of the roaming range, ...
            else if (position.x >= roamRightEnd.x)
            {
                targetVelocity = leftVelocity;      // Go to the left
                wasRoamingRight = false;
            }
            // Else targetVelocity is the same as previous fixedUpdate if position is within roaming ends

            // Determine what target it should be heading towards, especially if chased out of roaming zone
            // target could have been player
            if (wasRoamingRight)
            {
                // target = roamRightEnd
                if (position.x < roamRightEnd.x)
                    targetVelocity = rightVelocity;
                // else (position.x >= roamRightEnd.x)
            }
            else
            {
                // target = roamLeftEnd
                if (position.x > roamLeftEnd.x)
                    targetVelocity = leftVelocity;
                // else (position.x <= roamLeftEnd.x)
            }

            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, movementSmoothing);

        }
        else
        {
            // else chasing but on platform edge, remain stationary until player goes out of chasing range
            rb.velocity = Vector2.zero;
        }

        // Rotate mob according to its target
        if (targetVelocity.x < 0 && facingRight)
        {
            Flip();
        }
        else if (targetVelocity.x > 0 && !facingRight)
        {
            Flip();
        }

        // Switch animation state from idle to/from walking
        float speed = rb.velocity.x;
        animator.SetFloat("speed", speed < 0 ? -speed : speed);     // More efficient than Mathf.Abs()
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}