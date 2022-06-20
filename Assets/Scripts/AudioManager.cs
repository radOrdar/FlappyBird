using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    public enum ClipName
    {
        Die,
        Hit,
        Point,
        Swoosh,
        Wing
    }

    private Dictionary<ClipName, AudioClip> soundDictionary = new Dictionary<ClipName, AudioClip>();
    private AudioSource audioSource;

    protected override void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
            return;
        }

        base.Awake();

        foreach (var audioClip in Resources.LoadAll<AudioClip>("Audio"))
        {
            if (Enum.TryParse(audioClip.name, out ClipName clipName))
            {
                soundDictionary.Add(clipName, audioClip);
            }
        }

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(ClipName clipName)
    {
        audioSource.PlayOneShot(soundDictionary[clipName]);
    }
}