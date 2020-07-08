using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniRocksLogic : MonoBehaviour
{
    public float strength = 10;      // amount of force to shoot it out of mama rock

    // Start is called before the first frame update
    void OnEnable()
    {
        GetComponent<Rigidbody2D>().AddRelativeForce(transform.up*strength);
        Invoke("Die", 3f);
    }

    void Die()
    {
        GetComponent<MobHealth>().Die();
    }
}
