using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingAttack : MonoBehaviour
{
	public int attackDamage = 5;
	public int enragedAttackDamage = 8;

	public Vector3 attackOffset;
	public float attackRange = 1f;
	public LayerMask attackMask;
	public float teleportWaitTime;
	public bool countdown = true;
	public Transform enemy;
	private float waitTime = 0f;

	public void Update() {
		
		if (countdown) {
			waitTime += Time.deltaTime;
		}
		if (waitTime > teleportWaitTime) {
			TeleportAttackSummon();
			countdown = false;
		}
	}

	public void Attack() {
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null) {
			colInfo.GetComponent<PlayerHealth>().TakeDamage(attackDamage, true);
		}
	}

	public void EnragedAttack() {
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null) {
			colInfo.GetComponent<PlayerHealth>().TakeDamage(enragedAttackDamage, true);
		}
	}

	public void TeleportAttackSummon() {
		GetComponent<Animator>().SetBool("teleportAttack", true);
		waitTime = 0f;
		SummonMobs(enemy);
	}

	public void SummonMobs(Transform _enemy) {
		StartCoroutine(SummonChickenDelay(_enemy));
	}

	private IEnumerator SummonChickenDelay(Transform _enemy) {
		yield return new WaitForSeconds(1.5f);
		Instantiate(_enemy, new Vector3(55f, 2.75f, 0f), transform.rotation);
		Instantiate(_enemy, new Vector3(60f, 2.75f, 0f), transform.rotation);
		Instantiate(_enemy, new Vector3(65f, 2.75f, 0f), transform.rotation);
		Instantiate(_enemy, new Vector3(70f, 2.75f, 0f), transform.rotation);
		Instantiate(_enemy, new Vector3(72f, 2.75f, 0f), transform.rotation);
	}

	public bool EnemyCleared() {
		if (GameObject.FindGameObjectWithTag("spawned") == null) {
			countdown = true;
			return true;
		}
		return false;
	}

	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}
}