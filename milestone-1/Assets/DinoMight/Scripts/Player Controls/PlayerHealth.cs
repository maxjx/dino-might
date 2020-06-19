using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public int maxHealth = 5;
    public int currentHealth;
    public GameObject deathPrefab;  // Death animation, intentionally seperated from player
    public Respawner respawner;
    public HealthBar healthBar;     // This is a health bar object that needs to be created externally.

    private Animator animator;
    private Rigidbody2D m_rigidbody;
    private bool immune;

    void Awake()
    {
        animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // If player just started playing or static health was not set before or died and respawned
        if (Global.playerHealth == 0)
        {
            currentHealth = maxHealth;
            healthBar.setMaxHealth(maxHealth);
            Global.playerHealth = currentHealth;
        }
        else    // Scene change or resume playing, and health was recorded on global health and not 0
        {
            currentHealth = Global.playerHealth;
            healthBar.setMaxHealth(maxHealth);      // If player resumes playing, need to set max health value for the first time
            healthBar.setHealth(currentHealth);
        }
    }

    public void TakeDamage(int damage, bool attackRightwards)
    {
        if (immune)
        {
            return;
        }

        currentHealth -= damage;
        Global.playerHealth = currentHealth;
        healthBar.setHealth(currentHealth);     // Edited for health bar inclusion

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
        currentHealth = 0;
        Global.playerHealth = currentHealth;
        healthBar.setHealth(currentHealth);

        Instantiate(deathPrefab, transform.position, transform.rotation);

        // 2nd argument is spawnPointNumber, indicates where its corresponding spawn point is, cached in the Respawner
        // Player's spawnPointNumber = 0
        respawner.RespawnCharacter(gameObject, 0);

        gameObject.SetActive(false);
    }

    public void AddHealth(int health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.setHealth(currentHealth);
        Global.playerHealth = currentHealth;
    }

    public void Immunity(bool b)
    {
        immune = b;
    }
}
