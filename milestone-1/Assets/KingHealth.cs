using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingHealth : MonoBehaviour {
	public int health = 50;
	public GameObject deathEffect;
	public bool isInvulnerable = false;

	public void TakeDamage(int damage) {
		if (isInvulnerable) {
			return;
        }
		health -= damage;

//		if (health <= 200)
//		{
//			GetComponent<Animator>().SetBool("IsEnraged", true);
//		}

		if (health <= 0) {
			Die();
		}
	}

	void Die()
	{
		GetComponent<Animator>().SetBool("IsDead", true);
	}

}
