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
	private float waitTime = 0f;
	private float fistTime = 0f;
	private KingSummonFactory summonHelper;

	private void Start() {
		summonHelper = gameObject.GetComponent<KingSummonFactory>();
	}
	public void Update() {
		// King counts down only when visible
		if (countdown) {
			waitTime += Time.deltaTime;
			fistTime += Time.deltaTime;

		}
		// Countdown to next fist attack
		if (fistTime > Random.Range(4f, 7f)) {
			FistAttack();
		}
		// Countdown to next teleport attack
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

	private void FistAttack() {
		fistTime = 0f;
		summonHelper.Summon("fist");
	}

	private void TeleportAttackSummon() {
		GetComponent<Animator>().SetBool("teleportAttack", true);
		waitTime = 0f;
		summonHelper.Summon("chicken");
	}

	public bool EnemyCleared() {
		if (GameObject.FindGameObjectWithTag("spawned") == null) {
			countdown = true;
			return true;
		}
		return false;
	}

	private void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}
}