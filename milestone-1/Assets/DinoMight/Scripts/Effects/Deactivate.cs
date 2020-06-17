using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    public float duration = 0.5f;
    
    void OnEnable()
    {
        StartCoroutine(DelayedDeactivateCoroutine());
    }

    IEnumerator DelayedDeactivateCoroutine()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
