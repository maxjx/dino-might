using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;
    void Start()
    {
        AudioManager.Instance.PlayMusic(musicClip);
    }

}
