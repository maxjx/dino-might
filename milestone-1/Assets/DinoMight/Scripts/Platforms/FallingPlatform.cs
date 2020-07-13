using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 _position;
    [SerializeField] private float waitTime = 1.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _position = GetComponent<Transform>().position;
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
        BoxCollider2D _box = GetComponent<BoxCollider2D>();

        yield return new WaitForSeconds(waitTime);
        rb.isKinematic = false;
        _box.enabled = false;

        yield return new WaitForSeconds(2f);
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        _box.enabled = true;
        Debug.Log("called");
        GetComponent<Transform>().position = _position;
    }
}
