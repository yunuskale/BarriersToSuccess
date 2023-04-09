using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audioClipList;
    [SerializeField] private AudioSource audioSource, bgAudioSource;
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public enum AudioCallers
    {
        bgMusic1 = 0,
        win = 1,
        lose = 2,
        box = 3,
        item = 4,
        incorrect = 5,
    }

    public void PlaySoundEffects(AudioCallers audio)
    {
        audioSource.PlayOneShot(audioClipList[(int)audio]);
    }
    public void PlayBgSound()
    {
        bgAudioSource.clip = audioClipList[0];
        bgAudioSource.Play();
    }
    public void StopBgAudio()
    {
        bgAudioSource.Stop();
    }
}
