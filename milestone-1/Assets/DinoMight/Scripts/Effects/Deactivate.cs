using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    public float duration = 0.5f;
    
    void OnEnable()
    {
        Invoke("DeactivateThis", duration);
    }

    void DeactivateThis()
    {
        gameObject.SetActive(false);
    }
}
