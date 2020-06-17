using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPS : MonoBehaviour
{
    public ParticleSystem particleSystemPrefab;

    public void Play()
    {
        particleSystemPrefab.Play();
    }
}
