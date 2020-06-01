﻿using System.Collections;
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
    private Vector2 target;
    private Animator animator;
    private PlayerHealth playerHealth;
    private Rigidbody2D rigidBody;      // of this mob

    private float step;     // Distance per fixedUpdate used in calculating velocity change
    private Vector2 targetVelocity;         // Displacement per fixedUpdate
    private bool facingRight = true;        // Rotate the sprite to face target which can be a roaming-end or player
    private bool wasRoamingRight = true;    // To remember the end that it was heading towards so that it can turn back
    private float raycastDistance = 1f;     // Distance that raycast detects for ground
    private Vector2 currentVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerHealth = player.GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        Vector2 startingpt = transform.position;    // Rigidbody2D's Y position is frozen to maintain a straight line of roaming

        roamLeftEnd = new Vector2(startingpt.x - roamingRange, startingpt.y);
        roamRightEnd = new Vector2(startingpt.x + roamingRange, startingpt.y);

        step = speed * Time.fixedDeltaTime * 10f;
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;  // Current position
        Vector2 playerPos = player.position;

        bool groundHit = Physics2D.Raycast(groundDetector.position, Vector2.down, raycastDistance);     // Returns true if raycast hits a collider
        Vector2 displacementFromPlayer = playerPos - position;
        float sqdistanceFromPlayer = displacementFromPlayer.sqrMagnitude;   // Used square instead of Vector2.Distance for optimization
        bool withinChasingRange = sqdistanceFromPlayer <= chasingRange * chasingRange;

        // If player is within chasing range and this mob is on a platform, ...
        if (withinChasingRange && groundHit)
        {
            if (position.x - playerPos.x > 0.1f)   // Player is to the left
            {
                targetVelocity = new Vector2(-step, 0);
            }
            else if (position.x - playerPos.x < -0.1f)     // Player is to the right
            {
                targetVelocity = new Vector2(step, 0);
            }
            else    // position.x is close enough to playerPos.x
            {
                targetVelocity = Vector2.zero;   // zero velocity
            }
            rigidBody.velocity = Vector2.SmoothDamp(rigidBody.velocity, targetVelocity * 2, ref currentVelocity, movementSmoothing); // To chase faster
        }
        // Not chasing and should return to opposite roam end
        else if (!withinChasingRange)
        {
            // If current position is further left or at the left end of the roaming range, ...
            if (position.x <= roamLeftEnd.x)
            {
                targetVelocity = new Vector2(step, 0);     // Go to the right
                wasRoamingRight = true;
            }   // If current position is further right or at the right end of the roaming range, ...
            else if (position.x >= roamRightEnd.x)
            {
                targetVelocity = new Vector2(-step, 0);      // Go to the left
                wasRoamingRight = false;
            }
            // Else targetVelocity is the same as previous fixedUpdate if position is within roaming ends

            // Determine what target it should be heading towards, especially if chased out of roaming zone
            // target could have been player
            if (wasRoamingRight)
            {
                // target = roamRightEnd
                if (position.x < roamRightEnd.x)
                    targetVelocity = new Vector2(step, 0);
                // else (position.x >= roamRightEnd.x)
            }
            else
            {
                // target = roamLeftEnd
                if (position.x > roamLeftEnd.x)
                    targetVelocity = new Vector2(-step, 0);
                // else (position.x <= roamLeftEnd.x)
            }

            rigidBody.velocity = Vector2.SmoothDamp(rigidBody.velocity, targetVelocity, ref currentVelocity, movementSmoothing);
        
        }   // else chasing but on platform edge, remain stationary until player goes out of chasing range
        else
        {
            rigidBody.velocity = Vector2.zero;
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
        if (sqdistanceFromPlayer < 0.1f && player.gameObject.activeInHierarchy)
        {
            // Set condition to animation state that invokes Hurt() via an event, to time attack frequency
            animator.SetTrigger("attack");
        }
    }

    public void Hurt()
    {
        // activeInHierarchy ensures that player does not takedamage while dead, else might respawn with low health
        if (player.gameObject.activeInHierarchy)
        {
            playerHealth.TakeDamage(damage);
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
    }
}