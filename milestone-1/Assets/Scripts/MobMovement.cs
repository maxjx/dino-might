using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    public float roamingRange = 1f;
    public float chasingRange = 3f; // Maximum distance from spawn point at which mob starts chasing player
    public float speed = 0.5f;
    public int damage = 1;
    public int health = 1;

    // Coordinates of roaming boundary ends and player GameObject
    private Vector2 roamLeftEnd;
    private Vector2 roamRightEnd;
    private Transform player;
    private Vector2 target;

    private float step = 0;    // Distance per frame used in the function MoveTowards
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Vector2 startingpt = transform.position;    // Rigidbody2D's Y position is frozen to maintain a straight line of roaming

        roamLeftEnd = new Vector2(startingpt.x - roamingRange, startingpt.y);
        roamRightEnd = new Vector2(startingpt.x + roamingRange, startingpt.y);

        step = speed * Time.deltaTime;
        target = roamRightEnd;     // Direction to move when spawned
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;  // Current position
        Vector2 playerPos = player.position;
        float distanceFromPlayer = Vector2.Distance(playerPos, position);

        // If current position is further left or at the left end of the roaming range, ...
        if (position.x <= roamLeftEnd.x)
        {
            target = roamRightEnd;     // Go to the right
        }   // If current position is further right or at the right end of the roaming range, ...
        else if (position.x >= roamRightEnd.x)
        {
            target = roamLeftEnd;      // Go to the left
        }   
        
        // If player is within chasing range, ...
        if (distanceFromPlayer <= chasingRange)
        {
            Vector2 newTarget = new Vector2(playerPos.x, position.y);  // Chase player instead
            transform.position = Vector2.MoveTowards(transform.position, newTarget, step);
        } else {
            transform.position = Vector2.MoveTowards(transform.position, target, step);
        }

        // Rotate mob according to its target
        if (position.x - target.x < 0 && !facingRight)
        {
            Flip();
        }
        else if (position.x - target.x > 0 && facingRight)
        {
            Flip();
        }

        if (distanceFromPlayer < 0.5f) {
            // Set condition to animation state that invokes Hurt(), to time attack frequency

        }
    }

    public void Hurt() {
        player.GetComponent<Health>().TakeDamage(damage);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
