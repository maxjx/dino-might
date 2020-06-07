using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public Transform firePoint;     // Point at which the bulletPrefab appears
    public Transform kickPoint;     // Point at which the kick hits another object
    public GameObject kickHitEffect; // When the kick hits an enemy
    public float kickRange = 0.5f;
    public int kickDamage = 1;
    public LayerMask enemyLayers;   // Contains info of all objects with specified layer

    private float attackRate = 0.2f;   // Time taken to attack again
    private float timer = 0f;
    private bool kick = false;      // To pass button input from Update into FIxedUpdate
    private bool shoot = false;     // To pass button input from Update into FIxedUpdate
    private Transform playerTransform;    // Player position to find relative direction of attack
    private bool attackRightwards;  // If true, player is attacking to the right
    private playerMovement playerMovementControl;
    private Animator animator;

    void Start()
    {
        playerTransform = GetComponent<Transform>();     // Cannot simply use transform.position
        playerMovementControl = GetComponent<playerMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && timer >= attackRate)
        {
            shoot = true;
            timer = 0;
        }
        if (Input.GetButtonDown("Fire2") && timer >= attackRate)
        {
            kick = true;     // IMPERFECT KICK TRANSITION (take into account crouching and knockback and jumping)
            timer = 0;
        }
    }

    void FixedUpdate()
    {
        animator.ResetTrigger("kick");
        if (shoot)
        {
            // Spawns Fireball object at the position and rotation of the firePoint
            ObjectPooler.Instance.SpawnFromPool("Fireball", firePoint.position, firePoint.rotation);
            playerMovementControl.CrouchOnce();     // For shooting animation

            shoot = false;
        }
        if (kick)
        {
            animator.SetTrigger("kick");
            ObjectPooler.Instance.SpawnFromPool("Kick", kickPoint.position, kickPoint.rotation);

            // Detect enemies in a circle with center kickpoint and radius kickRange (AOE attack)
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(kickPoint.position, kickRange, enemyLayers);

            // Determine relative direction of attack
            if (playerTransform.position.x < kickPoint.position.x)
            {
                attackRightwards = true;
            }
            else
            {
                attackRightwards = false;
            }

            // Damage all enemies in range
            foreach (Collider2D enemy in hitEnemies)
            {
                Instantiate(kickHitEffect, kickPoint.position, kickPoint.rotation);
                enemy.GetComponent<IHealth>().TakeDamage(kickDamage, attackRightwards);
            }

            kick = false;
        }
    }
}
