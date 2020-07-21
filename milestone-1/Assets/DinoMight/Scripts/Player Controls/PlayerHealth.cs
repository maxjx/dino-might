using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public int maxHealth = 15;
    public int currentHealth;
    public GameObject deathPrefab;  // Death animation, intentionally seperated from player
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
        maxHealth = Global.playerMaxHealth;
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

        currentHealth = Health.MinusHealth(currentHealth, damage);
        Global.playerHealth = currentHealth;
        healthBar.setHealth(currentHealth);     // Edited for health bar inclusion

        animator.SetTrigger("hurt");

        // screen shake
        if (CinemachineShake.Instance != null)
        {
            CinemachineShake.Instance.ShakeCamera(2f, 0.1f);
        }

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

        // screen shake
        if (CinemachineShake.Instance != null)
        {
            CinemachineShake.Instance.ShakeCamera(3f, 0.1f);
        }

        if (deathPrefab != null)
            Instantiate(deathPrefab, transform.position, transform.rotation);

        GetComponent<PlayerTransition>().ManualTransition();
    }

    public void AddHealth(int amount)
    {
        currentHealth = Health.AddHealth(maxHealth, currentHealth, amount);
        healthBar.setHealth(currentHealth);
        Global.playerHealth = currentHealth;
    }

    public void Immunity(bool b)
    {
        immune = b;
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        healthBar.setMaxHealth(maxHealth);
        Global.playerMaxHealth = maxHealth;
        Global.playerHealth = maxHealth;
    }
}
