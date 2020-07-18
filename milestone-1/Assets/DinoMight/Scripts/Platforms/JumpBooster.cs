using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to propel any rigidbody by the amount of force input. Results vary. Best used for only vertical acceleration.
public class JumpBooster : MonoBehaviour
{
    public float horizontalForce = 500f;
    public float verticalForce = 500f;
    private Animator animator;
    private bool used = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        used = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
        if (rb != null && !used && collider.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(horizontalForce, verticalForce));

            used = true;
            animator.SetTrigger("used");
        }
    }

    void CallRespawner()
    {
        // Will set inactive, then set active after a few seconds
        if (Respawner.Instance != null)
        {
            Respawner.Instance.RespawnThing(gameObject);
        }
    }
}
