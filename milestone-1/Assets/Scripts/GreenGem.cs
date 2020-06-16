using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGem : MonoBehaviour
{
    public int addHealth = 1;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<PlayerHealth>().AddHealth(addHealth);
            animator.SetTrigger("used");
        }
    }

    void CallDestroy()
    {
        Destroy(gameObject);
    }
}
