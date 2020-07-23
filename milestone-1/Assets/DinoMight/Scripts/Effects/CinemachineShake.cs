using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineBasicMultiChannelPerlin noise;
    private float startIntensity = 0;
    private float shakeTimer = 0;
    private float shakeTimerTotal = 0;

    void Awake()
    {
        Instance = this;
        noise = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float duration)
    {
        startIntensity = intensity;
        shakeTimer = duration;
        shakeTimerTotal = duration;
    }

    // // use this in the class that causes the shake
    // // screen shake
    //     if (CinemachineShake.Instance != null)
    //     {
    //         CinemachineShake.Instance.ShakeCamera(0.5f, 0.2f);
    //     }

    public void BigShake()
    {
        ShakeCamera(4f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            noise.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0, 1 - (shakeTimer / shakeTimerTotal));
        }
    }
}
