using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InflictDamage : MonoBehaviour, IDamage
{
    public int damage;
    public float duration = 0.5f;
    private float timer = 0;
    private bool coolingDown = false;

    void FixedUpdate()
    {
        if (coolingDown)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= duration)
            {
                coolingDown = false;
                timer = 0;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!coolingDown & other.gameObject.CompareTag("Player"))
        {
            Damage(other);
            coolingDown = true;
        }
    }

    public void Damage(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<Rigidbody2D>().velocity.magnitude < 0)
            {
                other.GetComponent<IHealth>().TakeDamage(damage, true);
            }
            else
            {
                other.GetComponent<IHealth>().TakeDamage(damage, false);
            }
            Debug.Log("called");
        }
    }
}
