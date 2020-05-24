using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public Transform firePoint;     // Point at which the bulletPrefab appears
    public GameObject bulletPrefab; // The prefab used as a bullet when firing
    public Transform kickPoint;     // Point at which the kick hits another object
    public Animator animator;
    public float kickRange = 0.2f;
    public int kickDamage = 80;
    public LayerMask enemyLayers;   // Contains info of all objects with specified layer

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Kick();     // IMPERFECT KICK TRANSITION (take into account crouching and knockback and jumping)
        }
    }

    void Shoot()
    {
        // Creates a bulletPrefab object at the position and rotation of the firePoint
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void Kick()
    {
        animator.SetTrigger("Kick");

        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(kickPoint.position, kickRange, enemyLayers);

        // Damage all enemies in range
        foreach (Collider2D enemy in hitEnemies)
        {
            // code to damage enemy by calling enemy script component but theres no enemy created yet
            // enemy.GetComponent<Enemy>().TakeDamage(kickDamage);    // TakeDamage has to be a public method
        }
    }
}
