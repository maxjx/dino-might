using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InflictDamage : MonoBehaviour, IDamage
{
    public int damage;
    public float duration = 0.5f;
    private float timer = 0;
    protected bool coolingDown = false;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (!coolingDown & other.gameObject.CompareTag("Player"))
        {
            Debug.Log("damage");
            Damage(other);
            coolingDown = true;
        }
    }

    public void Damage(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("card damage");
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
