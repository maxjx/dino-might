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
    private Vector3 initialPosition;

    void Start()
    {
        // Records initial position to calculate distance to destroy this fireball later on
        initialPosition = transform.position;

        // Gives the rigidbody assigned to this GameObject a velocity of speed in the direction of right, which is the red axis
        rb.velocity = transform.right * speed;
    }

    // Called when this GameObject enters the trigger collider of another GameObject, basically they collide
    // hitInfo refers to the GameObject which is hit
    void OnTriggerEnter2D(Collider2D hitInfo) {
        // Only hits one enemy, if any. See Attack.kick() for AOE damage
        if (hitInfo.tag == "Enemy")
        {
            hitInfo.GetComponent<Health>().TakeDamage(damage);
        }
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void FixedUpdate() {
        // Destroys GameObject after set displacement to free heap space and prevent undesired effects outside screen
        if (Vector3.Distance(initialPosition, transform.position) > travellingDistance) {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
