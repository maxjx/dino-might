using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Attack : MonoBehaviour, IDamage
{
    public int damage = 2;
    public float damageRange = 0.5f;
    public Transform attackPoint;
    public Transform jumpAttackPoint;
    public Transform jumpForwardPoint;
    public LayerMask attackableLayers;      // Will deal damage to these layers, mainly just character

    // inflict damage at explicitly stated point, used in animation states
    void DamageAtSomePoint(int num)
    {
        Collider2D collider;
        switch (num)
        {
            case 0:
                collider = Physics2D.OverlapCircle(attackPoint.position, damageRange, attackableLayers);
                break;
            case 1:
                collider = Physics2D.OverlapCircle(jumpAttackPoint.position, damageRange, attackableLayers);
                break;
            case 2:
                collider = Physics2D.OverlapCircle(jumpForwardPoint.position, damageRange, attackableLayers);
                break;
            default:
                collider = null;
                break;
        }

        if (collider != null)
        {
            Damage(collider);
        }
    }

    public void Damage(Collider2D collider)
    {
        if (transform.position.x < attackPoint.position.x)
        {
            collider.GetComponent<IHealth>().TakeDamage(damage, true);    // attack rightwards
        }
        else
        {
            collider.GetComponent<IHealth>().TakeDamage(damage, false); // attack leftwards
        }
    }
}
