using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    private Animator animator;
    private Rigidbody2D rigidbody;

    void Start() {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage) {
        rigidbody.AddForce(new Vector2(300f, 200f));
        currentHealth -= damage;
        animator.SetTrigger("hurt");
        if (currentHealth <= 0) {
            animator.SetBool("dead", true);     // Animator state calls Die() manually
        }
    }

    // mobNumber indicates where its corresponding spawn point is, cached in the Respawner
    // Player's mobNumber = 0
    void Die(int mobNumber) {
        gameObject.SetActive(false);
        //Instantiate(deadprefab
        Respawner.RespawnCharacter(gameObject, mobNumber);
        currentHealth = maxHealth;
    }
}