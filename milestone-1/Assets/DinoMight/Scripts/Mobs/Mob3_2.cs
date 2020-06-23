using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob3_2 : MonoBehaviour
{
    public float speed = 1f;
    public float roamingRange = 2f;     // Roams this distance away from the starting point and turns around
    public float attackPlayerRange = 6f;
    // public float laserDistance = 6f;
    // public int laserDamage = 3;
    // public Transform laserFirePoint;
    // public LineRenderer laserAim;       // Laser that does not hurt
    // public LineRenderer laserBurst;     // Laser that hurts
    // public LayerMask whatCanHit;        // Defines which layers can the laser hit, other than the player
    // public LayerMask whatCanDamage;
    // public ParticleSystem laserEmission;
    // public ParticleSystem laserCollide; // Collide with floor etc

    private Animator animator;
    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 rightEnd;
    private Vector2 leftEnd;
    private bool movingRight = true;
    private Vector2 targetPos;          // The target position is should move towards
    private bool shooting = false;
    private float step;                 // speed * fixed delta time

    // // Laser variables
    // Vector2 laserPos;
    // Vector2 playerPos;
    // Vector3 dir;        // direction of vector from laser pos to player pos
    // Vector2 endPoint;
    // float laserLength;  // after it shoots

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Vector2 startingpt = transform.position;    // Rigidbody2D's Y position is frozen to maintain a straight line of roaming

        leftEnd = new Vector2(startingpt.x - roamingRange, startingpt.y);
        rightEnd = new Vector2(startingpt.x + roamingRange, startingpt.y);
        step = speed * Time.fixedDeltaTime;
    }

    void FixedUpdate()
    {
        Vector2 currPos = transform.position;
        if (player == null)
        {
            return;
        }

        // Determine if player is in attacking range
        Vector2 distanceFromPlayer = currPos - (Vector2)player.transform.position;
        float sqDisplacementFromPlayer = distanceFromPlayer.sqrMagnitude;
        bool playerInRange = sqDisplacementFromPlayer < attackPlayerRange * attackPlayerRange;
        bool facingPlayer = (distanceFromPlayer.x < 0 && movingRight) || (distanceFromPlayer.x > 0 && !movingRight);

        if (!shooting && playerInRange && facingPlayer)
        {
            StartCoroutine(ShootMissiles());
        }
        else if (!shooting)     // and player not in range and not facing player
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
                targetPos = Vector2.MoveTowards(rb.position, rightEnd, step);
            }
            else
            {
                targetPos = Vector2.MoveTowards(rb.position, leftEnd, step);
            }

            rb.MovePosition(targetPos);
        }
        // else shooting, dont move
    }

    IEnumerator ShootMissiles()
    {
        shooting = true;
        animator.SetTrigger("attack");

        yield return null;

        shooting  = false;
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        transform.Rotate(0f, 180f, 0f);
    }

    void OnDisable()
    {
        CancelInvoke();
        // laserEmission.Stop();
        // laserCollide.Stop();
        // laserAim.enabled = false;
        // laserBurst.enabled = false;
        shooting = false;
    }
}
