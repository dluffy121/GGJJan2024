using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource m_backgroundAudioSource;

    [SerializeField]
    AudioClip clickSound;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public static void PlaySoundEffect(AudioClip a_soundtoPlay)
    {
        Instance.m_backgroundAudioSource.PlayOneShot(a_soundtoPlay);
    }

    public void PlayClickSound()
    {
        m_backgroundAudioSource.PlayOneShot(clickSound);
    }
}
