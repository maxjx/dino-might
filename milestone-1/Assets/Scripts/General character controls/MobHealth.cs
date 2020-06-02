using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHealth : MonoBehaviour, IHealth
{
    public int maxHealth = 1;
    public int currentHealth;
    public GameObject deathPrefab;  // Death animation, intentionally seperated from character
    public Respawner respawner;

    private Animator animator;
    private Rigidbody2D m_rigidbody;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, bool attackRightwards)
    {
        currentHealth -= damage;
        animator.SetTrigger("hurt");

        // Determine direction of knockback
        if (attackRightwards)
        {
            m_rigidbody.AddForce(new Vector2(300f, 0));      // knockback to the right
        }
        else
        {
            m_rigidbody.AddForce(new Vector2(-300f, 0));      // knockback
        }
        
        if (currentHealth <= 0)
        {
            animator.SetBool("dead", true);     // Animator state calls Die() manually
        }
    }

    // spawnPointNumber indicates where its corresponding spawn point is, cached in the Respawner
    public void Die(int spawnPointNumber)
    {
        Instantiate(deathPrefab, transform.position, transform.rotation);

        respawner.RespawnCharacter(gameObject, spawnPointNumber);

        gameObject.SetActive(false);
    }
}

