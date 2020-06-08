using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to propel any rigidbody by the amount of force input. Results vary. Best used for only vertical acceleration.
public class JumpBooster : MonoBehaviour
{
    public float horizontalForce = 500f;
    public float verticalForce = 500f;
    public Respawner respawner;
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
        if (rb != null && !used)
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
        respawner.RespawnThing(gameObject);
    }
}
