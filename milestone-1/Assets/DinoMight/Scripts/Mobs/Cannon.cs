using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float fireRate = 1.5f;
    public float sustainedTime = 7f;
    private float timer;
    [SerializeField] private GameObject prefab;

    private void Start()
    {
        StartCoroutine(TimedDestroy());
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && timer > fireRate)
        {
            timer = 0f;
            float pos = other.gameObject.transform.position.x;
            Fire(pos);
        }
    }

    private void Fire(float pos)
    {
        float selfPos = pos - gameObject.transform.position.x;
        float xVelocity = selfPos/1.79f;
        GameObject bomb = (GameObject)Instantiate(prefab, transform.position, transform.rotation);

        bomb.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, 7.67f);
    }

    private IEnumerator TimedDestroy()
    {
        yield return new WaitForSeconds(sustainedTime);
        Destroy(gameObject);
    }
}
