using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Attack : MonoBehaviour, IDamage
{
    public int damage = 2;
    public float damageRange = 0.5f;
    public Transform attackPoint;
    public Transform jumpAttackGroundPoint;
    public Transform jumpAttackAirPoint;
    public Transform jumpForwardPoint;
    public LayerMask attackableLayers;      // Will deal damage to these layers, mainly just character

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // inflict damage at explicitly stated point, used in animation states
    void DamageAtSomePoint(int index)
    {
        Collider2D collider;
        switch (index)
        {
            // ground attack - stab
            case -1:
                collider = Physics2D.OverlapCircle(attackPoint.position, damageRange, attackableLayers);
                if (collider != null)
                {
                    animator.SetBool("stabbed", true);
                }
                else
                {
                    animator.SetBool("stabbed", false);
                }
                break;
            // ground attack - smash
            case 0:
                collider = Physics2D.OverlapCircle(attackPoint.position, damageRange, attackableLayers);
                break;
            // jumping attack
            case 1:
                RaycastHit2D hit = 
                    Physics2D.Raycast(
                        jumpAttackAirPoint.position,
                        (Vector2)(-jumpAttackAirPoint.position + jumpAttackGroundPoint.position),
                        Vector2.Distance(jumpAttackAirPoint.position, jumpAttackGroundPoint.position),
                        attackableLayers);
                collider = hit.collider;
                break;
            // jumping forward and smashing ground
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
