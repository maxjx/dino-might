using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public int maxHealth = 5;
    public static int currentHealth;
    public GameObject deathPrefab;  // Death animation, intentionally seperated from player
    public Respawner respawner;
    public HealthBar healthBar;     // This is a health bar object that needs to be created externally.

    private Animator animator;
    private Rigidbody2D m_rigidbody;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        healthBar.setMaxHealth(maxHealth);
    }

    void OnEnable()
    {
        currentHealth = maxHealth;
        healthBar.setHealth(currentHealth); 
    }

    public void TakeDamage(int damage, bool attackRightwards)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);     // Edited for health bar inclusion
        Debug.Log(currentHealth);               // For debugging

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

    public void Die()
    {
        Instantiate(deathPrefab, transform.position, transform.rotation);

        // 2nd argument is spawnPointNumber, indicates where its corresponding spawn point is, cached in the Respawner
        // Player's spawnPointNumber = 0
        respawner.RespawnCharacter(gameObject, 0);

        gameObject.SetActive(false);
    }
}
