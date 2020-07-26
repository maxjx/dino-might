using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityAttack : MonoBehaviour
{
    public float attackRange = 0.2f;
    public int damage = 1;

    private GameObject player;
    private Vector2 mobPosition;
    private Animator animator;
    private Vector2 playerPosition;
    private PlayerHealth playerHealth;
    private bool attackRightwards;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        mobPosition = transform.position;
        playerPosition = player.transform.position;

        Vector2 displacementFromPlayer = playerPosition - mobPosition;
        float sqdistanceFromPlayer = displacementFromPlayer.sqrMagnitude;

        if (sqdistanceFromPlayer < attackRange && player.activeInHierarchy)
        {
            // Set condition to animation state that invokes Hurt() via an event, to time attack frequency
            if (animator != null)
                animator.SetTrigger("attack");

            // Determine direction of attack
            if (playerPosition.x < mobPosition.x)
            {
                attackRightwards = false;
            }
            else
            {
                attackRightwards = true;
            }
        }
    }

    public void Hurt()
    {
        // activeInHierarchy ensures that player does not takedamage while dead, else might respawn with low health
        if (player.activeInHierarchy)
        {
            playerHealth.TakeDamage(damage, attackRightwards);
        }
    }

    // Used at the last frame of mob attack to prevent double count
    public void ResetTrigger()
    {
        animator.ResetTrigger("attack");
    }
}
