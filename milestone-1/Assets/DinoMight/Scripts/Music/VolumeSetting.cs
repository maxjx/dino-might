using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMusicVolume(float vol) {
        // Convert the volume slider to a linear adjustment since decibels is in log10
        vol = Mathf.Log10(vol) * 40;
        audioMixer.SetFloat("MusicVol", vol);
    }

    public void SetEffectsVolume(float vol) {
        // Convert the volume slider to a linear adjustment since decibels is in log10
        vol = Mathf.Log10(vol) * 40;
        audioMixer.SetFloat("EffectsVol", vol);
    }
}
