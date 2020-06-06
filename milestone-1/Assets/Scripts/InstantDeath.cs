using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to kill characters immediately
public class InstantDeath : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        IHealth health = collider.GetComponent<IHealth>();
        if (health != null)
        {
            health.Die();
        }
    }
}
