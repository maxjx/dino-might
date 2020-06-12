using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob1Movement : MonoBehaviour
{
    public float roamingRange = 1f;
    public float chasingRange = 3f;     // Maximum distance from spawn point at which mob starts chasing player
    public float speed = 5f;
    [Range(0, .3f)] public float movementSmoothing = 0.05f;	// How much to smooth out the movement
    public int damage = 1;
    public Transform groundDetector;    // Casts a ray from this point to detect ground so that it does not go off the platform

    // Coordinates of roaming boundary ends and player GameObject
    private Vector2 roamLeftEnd;
    private Vector2 roamRightEnd;
    private Transform player;
    private Animator animator;
    private PlayerHealth playerHealth;
    private Rigidbody2D rb;      // of this mob

    private float step;     // Distance per fixedUpdate used in calculating velocity change
    private Vector2 targetVelocity;         // Displacement per fixedUpdate
    private Vector2 leftVelocity;           // Displacement in the left direction per step
    private Vector2 rightVelocity;
    private Vector2 currentVelocity = Vector2.zero;
    private bool facingRight = true;        // Rotate the sprite to face target which can be a roaming-end or player
    private bool wasRoamingRight = true;    // To remember the end that it was heading towards so that it can turn back
    private float raycastDistance = 1f;     // Distance that raycast detects for ground
    private bool attackRightwards;          // If true, mob is attacking to the right
    private bool attacking = false;         // If true, mob is still in the attacking state and should not move especially when player is no longer in attack range

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            playerHealth = player.GetComponent<PlayerHealth>();
        }
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
            return;
        }
        Vector2 playerPos = player.position;

        bool groundHit = Physics2D.Raycast(groundDetector.position, Vector2.down, raycastDistance);     // Returns true if raycast hits a collider
        Vector2 displacementFromPlayer = playerPos - position;
        float sqdistanceFromPlayer = displacementFromPlayer.sqrMagnitude;   // Used square instead of Vector2.Distance for optimization
        bool withinChasingRange = sqdistanceFromPlayer <= chasingRange * chasingRange;

        // If player is within chasing range and this mob is on a platform, ...
        if (withinChasingRange && groundHit && !attacking)
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
        // Not chasing and should return to opposite roam end
        else if (!withinChasingRange && !attacking)
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
            // or attacking
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

        //animator.ResetTrigger("attack");
        // When mob is close enough to player, set trigger of hurt animation state
        if (sqdistanceFromPlayer < 0.2f && player.gameObject.activeInHierarchy)
        {
            // Set condition to animation state that invokes Hurt() via an event, to time attack frequency
            animator.SetTrigger("attack");

            // Stationary when attacking even when player goes out of attacking range
            rb.velocity = Vector2.zero;
            attacking = true;

            // Determine direction of attack
            if (playerPos.x < position.x)
            {
                attackRightwards = false;
            }
            else
            {
                attackRightwards = true;
            }
        }

        // Switch animation state from idle to/from walking
        float speed = rb.velocity.x;
        animator.SetFloat("speed", speed < 0 ? -speed : speed);     // More efficient than Mathf.Abs()
    }

    public void Hurt()
    {
        // activeInHierarchy ensures that player does not takedamage while dead, else might respawn with low health
        if (player.gameObject.activeInHierarchy)
        {
            playerHealth.TakeDamage(damage, attackRightwards);
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    // Used at the last frame of mob attack to prevent double count
    public void ResetTrigger()
    {
        animator.ResetTrigger("attack");
        attacking = false;
    }
}