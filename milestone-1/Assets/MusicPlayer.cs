using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip music1;
    void Start()
    {
        AudioManager.Instance.PlayMusic(music1);
    }

}
