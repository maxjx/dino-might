using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float waitTime = 1.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(Drop());
        }
    }

    private IEnumerator Drop()
    {
        yield return new WaitForSeconds(waitTime);
        rb.isKinematic = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
