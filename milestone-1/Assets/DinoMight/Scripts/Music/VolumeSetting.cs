using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicVolume;
    public Slider effectsVolume;

    private void Start()
    {
        musicVolume.value = Global.musicVolume;
        effectsVolume.value = Global.effectsVolume;
    }

    public void SetMusicVolume(float vol) {
        // The value saved to global is between 0.01 and 1
        Global.musicVolume = vol;

        // Convert the volume slider to a linear adjustment since decibels is in log10
        vol = Mathf.Log10(vol) * 40;
        audioMixer.SetFloat("MusicVol", vol);
    }

    public void SetEffectsVolume(float vol) {
        // The value saved to global is between 0.01 and 1
        Global.effectsVolume = vol;
        
        // Convert the volume slider to a linear adjustment since decibels is in log10
        vol = Mathf.Log10(vol) * 40;
        audioMixer.SetFloat("EffectsVol", vol);
    }
}
