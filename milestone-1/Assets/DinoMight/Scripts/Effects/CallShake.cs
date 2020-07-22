using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallShake : MonoBehaviour
{
    public float delay = 0f;
    public float intensity = 1f;
    public float duration = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Shake", delay);
    }

    void Shake()
    {
        // screen shake
        if (CinemachineShake.Instance != null)
        {
            CinemachineShake.Instance.ShakeCamera(intensity, duration);
        }
    }
}
