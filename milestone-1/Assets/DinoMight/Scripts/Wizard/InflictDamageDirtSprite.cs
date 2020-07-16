using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictDamageDirtSprite : InflictDamage
{
    public Vector3 boxSize;
    public LayerMask attackMask;

    public void ManualDamage()
    {
        Collider2D colInfo = Physics2D.OverlapBox(transform.position, boxSize, 0f, attackMask);
		if (colInfo != null) {
			colInfo.GetComponent<PlayerHealth>().TakeDamage(damage, true);
		}
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireCube(transform.position, boxSize);
	}
}
