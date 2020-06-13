using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGem : MonoBehaviour
{
    public int addHealth = 1;
    public Respawner respawner;
    private Animator animator;
    private bool used = false;

    void OnEnable()
    {
        used = false;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!used && collider.CompareTag("Player"))
        {
            collider.GetComponent<PlayerHealth>().AddHealth(addHealth);
            used = true;
            animator.SetTrigger("used");
        }
    }

    void CallRespawner()
    {
        // Will set inactive, then set active after a few seconds
        respawner.RespawnThing(gameObject);
    }
}
