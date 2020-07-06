using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob5Logic : MonoBehaviour
{
    public float distanceToExplode = 10f;     // minimum distance away from startPos to explode and die, will only explode on collision
    public ParticleSystem deathExplosion;
    public GameObject miniRockersParent;      // gameobject that stores the mini rockers as children to spawn

    private Vector2 startPos;           // starting position
    private Animator animator;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        float displacement = startPos.x - transform.position.x;
        float absdisplacement = displacement < 0 ? -displacement : displacement;
        if (distanceToExplode <= absdisplacement)
        {
            animator.SetBool("dead", true);
        }
    }

    void ExplodeWithMiniRocks()
    {
        deathExplosion.transform.position = transform.position;
        deathExplosion.Play();

        float angle = -30f;
        foreach (Transform child in miniRockersParent.transform)
        {
            child.position = transform.position;
            child.rotation = Quaternion.Euler(0,0,angle);
            angle += 30f;
            child.gameObject.SetActive(true);
        }
    }
}