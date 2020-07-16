using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : MonoBehaviour
{
    public int attackDamage = 5;
	public Vector3 attackOffset;
	public float attackRange = 1f;
	public LayerMask attackMask;
	public float teleportWaitTime;
	public bool countdown = true;
	private float attackTime = 0f;
	private float teleportTime = 0f;
    private WizardSummonFactory summonHelper;

	private void Start() {
		summonHelper = gameObject.GetComponent<WizardSummonFactory>();
	}

    public void Update() {
		// Wizard counts down only when visible
		if (countdown) {
			attackTime += Time.deltaTime;
			teleportTime += Time.deltaTime;
		}
		// Countdown to next range attack
		if (attackTime > Random.Range(4f, 7f)) {
			GeneralAttackTrigger();
		}
		// Countdown to next burst attack
//		if (teleportTime > teleportWaitTime) {
//			countdown = false;
//		}
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

    private void GeneralAttackTrigger()
    {
        attackTime = 0f;
        GetComponent<Animator>().SetBool("generalAttack", true);
    }

    public void GeneralAttack()
    {
        string[] attackTypes = new string[] {"ranged"};
		int result = Random.Range(0, 1);
		summonHelper.Summon(attackTypes[result]);
    }

    private void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}
}
