using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnEnable : Deactivate
{
    public float duration = 0.5f;
    
    void OnEnable()
    {
        Invoke("DeactivateThis", duration);
    }

}
