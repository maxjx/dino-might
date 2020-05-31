using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public int maxHealth = 5;
    public static int currentHealth;
    public GameObject deathPrefab;  // Death animation, intentionally seperated from player
    public Respawner respawner;

    private Animator animator;
    private Rigidbody2D m_rigidbody;

    void Start() {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    
    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        m_rigidbody.AddForce(new Vector2(-300f, 300f));     // Knockback
        currentHealth -= damage;

        Debug.Log(currentHealth);
        
        animator.SetTrigger("hurt");
        if (currentHealth <= 0) {
            animator.SetBool("dead", true);     // Animator state calls Die() manually
        }
    }

    // spawnPointNumber indicates where its corresponding spawn point is, cached in the Respawner
    // Player's spawnPointNumber = 0
    public void Die(int spawnPointNumber) {
        Instantiate(deathPrefab, transform.position, transform.rotation);

        respawner.RespawnCharacter(gameObject, 0);

        gameObject.SetActive(false);
    }
}
