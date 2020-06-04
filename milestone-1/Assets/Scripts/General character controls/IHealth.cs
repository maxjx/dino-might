using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interfaces conventionally start with I
public interface IHealth
{
    // If attackRightwards is true, the character inflicting damage is to the left of the character taking damage
    // Affects direction of knockback
    void TakeDamage(int damage, bool attackRightwards);

    void Die();
}