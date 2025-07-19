using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance { private set; get; }
    [SerializeField] private AudioSource audioSourceMusic;
    [SerializeField] private AudioSource audioSourceSFX;
    [SerializeField] private AudioSource audioSourceUI;
    [SerializeField] private AudioMixer audioMixer;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

        }
        else
        {
            Instance = this;

        }
    }
    public List<AudioComponent> DicAudioClip = new List<AudioComponent>();
    public void ActiveAudioClip(string tag)
    {
        AudioComponent _audioComponent = DicAudioClip.Find(p => p.TagAudio == tag);

        if (_audioComponent != null)
        {
            if (_audioComponent.TagAudioSource == TagAudioSource.Music)
            {
                audioSourceMusic.clip = _audioComponent.AudioClip;
                audioSourceMusic.loop = true;
                audioSourceMusic.Play();
            }
            else if (_audioComponent.TagAudioSource == TagAudioSource.SFX)
            {
                audioSourceSFX.clip = _audioComponent.AudioClip;
                audioSourceSFX.loop = false;
                audioSourceSFX.PlayOneShot(_audioComponent.AudioClip);
            }
            else if (_audioComponent.TagAudioSource == TagAudioSource.UI)
            {
                audioSourceUI.clip = _audioComponent.AudioClip;
                audioSourceUI.loop = false;
                audioSourceUI.PlayOneShot(_audioComponent.AudioClip);
            }
            else return;
        }
        else return;
    }
}
public enum TagAudioSource
{
    Music,
    SFX,
    UI

}
[Serializable]
public class AudioComponent
{
    public string TagAudio;
    public TagAudioSource TagAudioSource;
    public AudioClip AudioClip;
}