using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHealth : MonoBehaviour, IHealth
{
    [Tooltip("Like an ID for the mob. Tells respawner which spawn point to spawn at.")]
    public int spawnNumber;
    public int maxHealth = 1;
    public int currentHealth;
    public ParticleSystem deathExplosion;
    public Respawner respawner;
    public bool allowKnockback = true;

    private Animator animator;
    private Rigidbody2D m_rigidbody;
    private bool wasHurt = false;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        currentHealth = maxHealth;
        wasHurt = false;
    }

    public void TakeDamage(int damage, bool attackRightwards)
    {
        currentHealth -= damage;
        animator.SetTrigger("hurt");
        wasHurt = true;

        if (allowKnockback)
        {
            // Determine direction of knockback
            if (attackRightwards)
            {
                m_rigidbody.AddForce(new Vector2(300f, 0));      // knockback to the right
            }
            else
            {
                m_rigidbody.AddForce(new Vector2(-300f, 0));      // knockback
            }
        }

        if (currentHealth <= 0)
        {
            animator.SetBool("dead", true);     // Animator state calls Die() manually
        }
    }

    // spawnNumber indicates where its corresponding spawn point is, cached in the Respawner
    public void Die()
    {
        deathExplosion.transform.position = transform.position;
        deathExplosion.Play();

        if (respawner != null)
        {
            respawner.RespawnCharacter(gameObject, spawnNumber);
        }

        gameObject.SetActive(false);
    }

    public bool WasHurt()
    {
        return wasHurt;
    }
}

