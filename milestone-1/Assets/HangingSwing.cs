using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingSwing : MonoBehaviour {

    public Rigidbody2D body;
    public EdgeCollider2D edgeCollider;
    public float leftPushRange;
    public float rightPushRange;
    public float maxVelocity;
    public int damage;
    void Start() {
        body = GetComponent<Rigidbody2D>();
        body.angularVelocity = maxVelocity;
    }

    void Update() {
        // On every update the pendulum should have some force pushing?
        Push();
    }

    private void Push() {
        // Let the force only be pushing near the bottom and let natural gravity pull it back
        if (transform.rotation.z < 0 && transform.rotation.z > leftPushRange
                && body.angularVelocity > maxVelocity * -1 && body.angularVelocity < 0) {
            body.angularVelocity = maxVelocity * -1;
        } else if (transform.rotation.z > 0 && transform.rotation.z > rightPushRange
                && body.angularVelocity < maxVelocity && body.angularVelocity > 0) { 
            body.angularVelocity = maxVelocity;
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            GameObject player = other.gameObject;
            if (body.angularVelocity < 0) {
                player.GetComponent<PlayerHealth>().TakeDamage(damage, false);
            } else {
                player.GetComponent<PlayerHealth>().TakeDamage(damage, true);
            }
        }
    }

}
