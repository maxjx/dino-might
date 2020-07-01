using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : MonoBehaviour, IHealth
{
    public int maxHealth = 1;
    private int currHealth;
    public ParticleSystem deathExplosion;

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
        deathExplosion.transform.position = transform.position;
        deathExplosion.Play();

        gameObject.SetActive(false);
    }
}
