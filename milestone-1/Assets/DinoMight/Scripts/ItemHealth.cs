using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : MonoBehaviour, IHealth
{
    public int maxHealth = 1;
    private int currHealth;
    public GameObject deathExplosion;

    private void Start()
    {
        currHealth = maxHealth;
    }
    public void TakeDamage(int damage, bool rightwards)
    {
        currHealth = Health.MinusHealth(currHealth, damage);

        if (currHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Instantiate(deathExplosion);
        // deathExplosion.transform.position = transform.position;
        // deathExplosion.GetComponent<ParticleSystem>().Play();

        gameObject.SetActive(false);
    }
}
