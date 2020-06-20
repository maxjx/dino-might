using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingSwing : MonoBehaviour {

    private Rigidbody2D body;
    private HingeJoint2D joint;
    private float startAngle;
    public float range;
    public float maxVelocity;
    public int damage;
    void Start() {
        body = GetComponent<Rigidbody2D>();
        body.angularVelocity = maxVelocity;
        joint = GetComponent<HingeJoint2D>();
        startAngle = joint.jointAngle;
    }

    void FixedUpdate() {
        // On every update the pendulum should have some force pushing?
        Push();
    }

    private void Push() {
        // Assume that jointAngle becomes negative as it rotates to the right
        float tempAngle = joint.jointAngle - startAngle;

        // Let the force only be pushing near the bottom and let natural gravity pull it back
        if (tempAngle < 0 && tempAngle > -1 * range
                && body.angularVelocity < maxVelocity && body.angularVelocity > 0) {
            // Logic for right push
            body.angularVelocity = maxVelocity;
        } else if (tempAngle > 0 && tempAngle < range
                && body.angularVelocity > -1 * maxVelocity && body.angularVelocity < 0) {
            // Logic for left push
            body.angularVelocity = -1 * maxVelocity;
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
