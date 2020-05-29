using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob1Movement : MonoBehaviour
{
    public float roamingRange = 1f;
    public float chasingRange = 3f;     // Maximum distance from spawn point at which mob starts chasing player
    public float speed = 0.5f;
    public int damage = 1;
    public Transform groundDetector;    // Casts a ray from this point to detect ground so that it does not go off the platform

    // Coordinates of roaming boundary ends and player GameObject
    private Vector2 roamLeftEnd;
    private Vector2 roamRightEnd;
    private Transform player;
    private Vector2 target;
    private Animator animator;

    private float step;    // Distance per frame used in the function MoveTowards
    private bool facingRight = true;        // Rotate the sprite to face target which can be a roaming-end or player
    private bool wasRoamingRight = true;    // To remember the end that it was heading towards so that it can turn back
    private float raycastDistance = 1f;     // Distance that raycast detects for ground

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();

        Vector2 startingpt = transform.position;    // Rigidbody2D's Y position is frozen to maintain a straight line of roaming

        roamLeftEnd = new Vector2(startingpt.x - roamingRange, startingpt.y);
        roamRightEnd = new Vector2(startingpt.x + roamingRange, startingpt.y);

        step = speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;  // Current position
        Vector2 playerPos = player.position;

        bool groundHit = Physics2D.Raycast(groundDetector.position, Vector2.down, raycastDistance);     // Returns true if raycast hits a collider
        float sqdistanceFromPlayer = (playerPos - position).sqrMagnitude;   // Used square instead of Vector2.Distance for optimization
        bool withinChasingRange = sqdistanceFromPlayer <= chasingRange * chasingRange;

        // If player is within chasing range and this mob is on a platform, ...
        if (withinChasingRange && groundHit)
        {
            target = new Vector2(playerPos.x, position.y);  // Chase player instead
            transform.position = Vector2.MoveTowards(transform.position, target, step * 2); // To chase faster
        }
        // Not chasing and should return to opposite roam end
        else if (!withinChasingRange)
        {
            // If current position is further left or at the left end of the roaming range, ...
            if (position.x <= roamLeftEnd.x)
            {
                target = roamRightEnd;     // Go to the right
                wasRoamingRight = true;
            }   // If current position is further right or at the right end of the roaming range, ...
            else if (position.x >= roamRightEnd.x)
            {
                target = roamLeftEnd;      // Go to the left
                wasRoamingRight = false;
            }

            if (wasRoamingRight)
            {
                target = roamRightEnd;
            }
            else
            {
                target = roamLeftEnd;
            }
            transform.position = Vector2.MoveTowards(transform.position, target, step);
        }
        // else chasing but on platform edge, do nothing until player goes out of chasing range

        // Rotate mob according to its target
        if (position.x - target.x < 0 && !facingRight)
        {
            Flip();
        }
        else if (position.x - target.x > 0 && facingRight)
        {
            Flip();
        }

        // When mob is close enough to player, set trigger of hurt animation state
        if (sqdistanceFromPlayer < 0.1f * 0.1f && player.gameObject.activeInHierarchy)
        {
            // Set condition to animation state that invokes Hurt() via an event, to time attack frequency
            animator.SetTrigger("attack");
        }
    }

    public void Hurt()
    {
        animator.ResetTrigger("attack");
        if (player.gameObject.activeInHierarchy)
        {
            player.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
