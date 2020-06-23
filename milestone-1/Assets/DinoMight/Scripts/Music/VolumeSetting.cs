using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMusicVolume(float vol) {
        audioMixer.SetFloat("music", vol);
    }

    public void SetEffectsVolume(float vol) {
        AudioManager.Instance.SetEffectsVolume(vol);
    }
}
