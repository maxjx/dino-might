using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip music;

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            AudioManager.Instance.PlayMusicWithFade(music, 1f);
        }
    }
    public void ManualTransition() {
        AudioManager.Instance.PlayMusicWithFade(music, 1f);
    }
}
