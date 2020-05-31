using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interfaces conventionally start with I
public interface IHealth
{
    void TakeDamage(int damage);

    void Die(int spawnPointNumber);
}