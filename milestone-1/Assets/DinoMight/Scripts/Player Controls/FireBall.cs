using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour, IDamage
{
    public float speed = 20f;
    public int damage = 3;
    public Rigidbody2D rb;
    public float travellingDistance = 4;
    public LayerMask whatLayerCanHit;       // Specifies what layers can be hit by fireball, not damage
    public List<string> whatTagCanDamage;

    private Vector2 initialPosition;
    private bool attackRightwards;          // If true, fireball is heading to the right

    void OnEnable()
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

        if (whatLayerCanHit == (whatLayerCanHit | (1 << hitInfo.gameObject.layer)))
        {
            // Only hits one enemy, if any. See Attack.kick() for AOE damage
            if (whatTagCanDamage.Contains(hitInfo.tag)) //hitInfo.CompareTag("Enemy") || hitInfo.CompareTag("spawned"))
            {
                Damage(hitInfo);
            }

            // Animate impact effect on collision
            TrySpawnFireball();
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        float difference = initialPosition.x - transform.position.x;
        float displacement = difference > 0 ? difference : -difference;     // More efficient than Mathf.Abs()
        // Destroys GameObject after set displacement to free heap space and prevent undesired effects outside screen
        if (displacement > travellingDistance)
        {
            // Animate impact effect on collision
            TrySpawnFireball();
            gameObject.SetActive(false);
        }
    }

    public void Damage(Collider2D collider)
    {
        collider.GetComponent<IHealth>().TakeDamage(damage, attackRightwards);
    }

    // Try spawning fireball from object pooler
    private void TrySpawnFireball()
    {
        if (ObjectPooler.Instance != null)
        {
            ObjectPooler.Instance.SpawnFromPool("FireballImpact", transform.position, transform.rotation);
        }
    }

}
