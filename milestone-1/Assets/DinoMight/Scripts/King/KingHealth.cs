using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingHealth : MonoBehaviour, IHealth {
	public int health = 50;
	public bool isInvulnerable = false;
	public BossHealthBar healthBar;


	private void Start() {
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
		Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
		GetComponent<Animator>().SetBool("isDead", true);
	}

	private void DestroySelf() {
		Global.questNumber++;
		Destroy(gameObject);
	}

}
