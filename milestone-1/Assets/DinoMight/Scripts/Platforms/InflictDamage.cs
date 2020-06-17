using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictDamage : MonoBehaviour {
    public int damage;

    public void OnEnterTrigger2D(Collider2D other) {
        Damage(other);
    }
    public void Damage(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (other.GetComponent<Rigidbody2D>().velocity.magnitude < 0) {
                other.GetComponent<IHealth>().TakeDamage(damage, true);
            } else {
                other.GetComponent<IHealth>().TakeDamage(damage, false);
            }
        }
    }
}
