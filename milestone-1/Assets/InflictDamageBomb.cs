using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictDamageBomb : MonoBehaviour
{
    public int damage;
    private bool firstAttack = true;

    private void Start()
    {
        StartCoroutine(WaitToDestroy());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (firstAttack & other.gameObject.CompareTag("Player"))
        {
            Damage(other);
            firstAttack = false;
            StartCoroutine(DestroySelf());
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
        }
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }
}
