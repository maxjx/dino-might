using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingHealth : MonoBehaviour, IHealth {
	public int health = 50;
	public GameObject deathEffect;
	public bool isInvulnerable = false;

	private GameObject tree;

	private void Start() {
		tree = GameObject.FindGameObjectWithTag("TeleportTree");

		tree.SetActive(false);
	}

	public void TakeDamage(int damage, bool attackRightwards) {
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

	public void Die()
	{
		GetComponent<Animator>().SetBool("isDead", true);
	}

	void TeleportTree() {
		tree.SetActive(true);
	}

}
