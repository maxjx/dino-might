using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 3;
    public Rigidbody2D rb;
    public GameObject impactEffect; // A GameObject just to animate the impact effect at the point of collision
    public float travellingDistance = 2;
    private Vector2 initialPosition;
    private bool attackRightwards; // If true, fireball is heading to the right

    void Start()
    {
        // Records initial position to calculate distance to destroy this fireball later on
        initialPosition = transform.position;

        // Gives the rigidbody assigned to this GameObject a velocity of speed in the direction of right, which is the red axis
        rb.velocity = transform.right * speed;
    }

    // Called when this GameObject enters the trigger collider of another GameObject, basically they collide
    // hitInfo refers to the GameObject which is hit
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Determine relative direction of attack
        if (initialPosition.x < transform.position.x)
        {
            attackRightwards = true;
        }
        else
        {
            attackRightwards = false;
        }

        // Only hits one enemy, if any. See Attack.kick() for AOE damage
        if (hitInfo.CompareTag("Enemy"))
        {
            hitInfo.GetComponent<IHealth>().TakeDamage(damage, attackRightwards);
        }
        
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        // Destroys GameObject after set displacement to free heap space and prevent undesired effects outside screen
        if (initialPosition.x - transform.position.x > travellingDistance)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
