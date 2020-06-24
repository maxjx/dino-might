using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicLoader : MonoBehaviour
{
    [SerializeField] private AudioClip music1;

    public AudioClip[] musicList;
    private void Start() {
    }

    public void PlayMusic(int index) {
        if (index > SceneManager.sceneCountInBuildSettings - 1) {
            Debug.Log("Scene index out of bound");
        }
        AudioManager.Instance.PlayMusicWithFade(musicList[index], 1f);
    }
}
