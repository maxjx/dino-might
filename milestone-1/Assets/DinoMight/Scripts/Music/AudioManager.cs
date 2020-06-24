using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region instance
    private static AudioManager _instance;
    public static AudioManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<AudioManager>();
                if (_instance == null) {
                    _instance = new GameObject("Created AudioManager", typeof(AudioManager))
                        .GetComponent<AudioManager>();
                }
            }
            return _instance;
        }
        private set {
                _instance = value;
        }
    }
    #endregion

    #region Fields

    public AudioMixerGroup masterGroup;
    public AudioMixer masterMixer;
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup effectsGroup;
    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource effectSource;
    private int currSource = 0;
    private float musicVolume = 1f;
    #endregion

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);

        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        effectSource = this.gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource2.loop = true;
    }

    public void PlayMusic(AudioClip musicClip) {
        AudioSource activeSource = (currSource == 0)
            ? musicSource
            : musicSource2;

        activeSource.clip = musicClip;
        activeSource.outputAudioMixerGroup = musicGroup;
        activeSource.Play();
    }
    public void PlayMusicWithFade(AudioClip newClip, float transitionTime) {
        AudioSource activeSource = (currSource == 0)
            ? musicSource
            : musicSource2;

        activeSource.outputAudioMixerGroup = musicGroup;
        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
    }
    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime) {
        if (!activeSource.isPlaying) {
            activeSource.Play();
        }

        for (float t = 0f; t < transitionTime; t += Time.deltaTime) {
            activeSource.volume = (musicVolume - (t / transitionTime) * musicVolume);
            yield return null;
        }

        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        for (float t = 0f; t < transitionTime; t += Time.deltaTime) {
            activeSource.volume = (t / transitionTime) * musicVolume;
            yield return null;
        }
    }

    public void PlayEffect(AudioClip musicClip) {
        effectSource.outputAudioMixerGroup = effectsGroup;
        effectSource.PlayOneShot(musicClip);
    }
    public void PlayEffect(AudioClip musicClip, float volume) {
        effectSource.outputAudioMixerGroup = effectsGroup;
        effectSource.PlayOneShot(musicClip, volume);
    }
}
