using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingHealth : MonoBehaviour, IHealth {
	public int health = 50;
	public GameObject deathEffect;
	public bool isInvulnerable = false;
	public BossHealthBar healthBar;

	private GameObject tree;

	private void Start() {
		tree = GameObject.FindGameObjectWithTag("TeleportTree");

		tree.SetActive(false);
		healthBar.setMaxHealth(health);
	}

	public void TakeDamage(int damage, bool attackRightwards) {
		if (isInvulnerable) {
			return;
        }

		health -= damage;
		healthBar.setHealth(health);

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
