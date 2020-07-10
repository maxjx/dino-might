using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeating : MonoBehaviour
{
    public string objToRepeatSpawn;          // repeatedly spawns this
    public Vector3 offset;      // offset its spawn position

    void OnEnable()
    {
        InvokeRepeating("Spit", 0, 4f);
    }

    void Spit()
    {
        if (ObjectPooler.Instance!=null)
        {
            ObjectPooler.Instance.SpawnFromPool(objToRepeatSpawn, transform.position + offset, transform.rotation);
        }
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
