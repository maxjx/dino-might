using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeMusic : MonoBehaviour
{
    public AudioClip music;
    public AudioClip fakeMusic;

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && GetComponent<InteractSceneTransition>() == null && Global.secretMoonlightMusic) {
            AudioManager.Instance.PlayMusicWithFade(music, 1f);
        }
        else if (other.CompareTag("Player") && GetComponent<InteractSceneTransition>() == null)
        {
            AudioManager.Instance.PlayMusicWithFade(fakeMusic, 1f);
        }
    }
}
