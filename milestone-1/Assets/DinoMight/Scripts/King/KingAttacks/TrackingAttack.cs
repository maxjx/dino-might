using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TrackingAttack : MonoBehaviour
{
	public float speed = 6f;
	public float rotateSpeed = 250f;
	private Rigidbody2D rb;
    private Transform player;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

	private void FixedUpdate()
    {
		Vector2 direction = (Vector2)player.position - rb.position;
		direction.Normalize();
		float rotateAmount = Vector3.Cross(direction, transform.up).z;
		rb.angularVelocity = -rotateAmount * rotateSpeed;
		rb.velocity = transform.up * speed;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DeleteObject());
        }
    }

    private IEnumerator DeleteObject()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }

    public void DeleteObjectCo()
    {
        StartCoroutine(DeleteObject());
    }
}
