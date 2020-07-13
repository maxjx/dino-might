using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaLogic : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<IHealth>().TakeDamage(1, true);
            Respawner.Instance.RespawnCharacter(collider.gameObject, 0);
        }
    }
}
