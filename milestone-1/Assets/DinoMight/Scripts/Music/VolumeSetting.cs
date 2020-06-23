using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSetting : MonoBehaviour
{
    public void SetMusicVolume(float vol) {
        AudioManager.Instance.SetMusicVolume(vol);
    }

    public void SetEffectsVolume(float vol) {
        AudioManager.Instance.SetEffectsVolume(vol);
    }
}
