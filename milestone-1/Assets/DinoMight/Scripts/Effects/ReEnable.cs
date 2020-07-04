using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReEnable : MonoBehaviour
{
    public Behaviour componentToReEnable;

    // Delay is the time from deactivation until activation
    public void ReEnableWithDelay(float delay)
    {
        //objToReactivate.SetActive(false);
        componentToReEnable.enabled = false;
        Invoke("Enable", delay);        // If time is set to 0, the method is invoked at the next Update cycle.
    }

    private void Enable()
    {
        //objToReactivate.SetActive(true);
        componentToReEnable.enabled = true;
    }
}
