using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologueManualTrigger : MonoBehaviour
{
    public Monologger monologue;

    void OnDisable()
    {
        if (monologue != null)
        {
            monologue.ManualTrigger();
        }
    }
}
