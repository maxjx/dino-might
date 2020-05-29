using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to make impact effect disappear after a certain time;
public class DestroyEffect : MonoBehaviour
{
    public float duration = 0.5f;

    void Start()
    {
        Destroy(gameObject, duration);
    }
}
