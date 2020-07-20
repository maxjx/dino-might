using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsPlayer : MonoBehaviour
{
    public AudioClip music;
    private bool playing = false;       // to prevent playing the same track more than once

    public void PlayEffect()
    {
        if (!playing)
        {
            AudioManager.Instance.PlayEffect(music);
            playing = true;
        }
    }

    public void UnpauseEffect()
    {
        if (!playing)
        {
            AudioManager.Instance.UnpauseEffect();
            playing = true;
        }
    }

    public void PauseEffect()
    {
        if (playing)
        {
            AudioManager.Instance.PauseEffect();
            playing = false;
        }
    }

    public void StopEffect()
    {
        AudioManager.Instance.StopEffect();
        playing = false;
    }

}
