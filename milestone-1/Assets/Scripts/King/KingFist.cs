using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingFist : MonoBehaviour
{
	public int attackDamage = 5;
	public Vector3 attackOffset;
	public LayerMask attackMask;

    public Vector3 attackSize;
    public GameObject me;


	public void attack() {
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapBox(pos, attackSize, 0, attackMask);
		if (colInfo != null && colInfo.gameObject.CompareTag("Player")) {
			colInfo.GetComponent<PlayerHealth>().TakeDamage(attackDamage, true);
		}
	}

    public void destroy() {
        Destroy(me);
    }

	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireCube(pos, attackSize);
	}
}