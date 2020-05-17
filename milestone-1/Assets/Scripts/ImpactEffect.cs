using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to make impact effect disappear after a certain time;
public class ImpactEffect : MonoBehaviour
{
    public float duration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(begone());
    }

    IEnumerator begone() {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
