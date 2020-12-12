using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioSource _audioSource;

    private Dictionary<string, AudioClip> _audioDictionary;

    protected override void OnAwake()
    {
        base.OnAwake();

        _audioDictionary = new Dictionary<string, AudioClip>();

        AudioClip[] clips = Resources.LoadAll<AudioClip>("Sounds");

        int len = clips.Length;

        for(int i = 0; i < len; i++)
        {
            AudioClip clip = clips[i];
            string clipName = clip.name;

            if (!_audioDictionary.ContainsKey(clipName))
            {
                _audioDictionary.Add(clipName, clip);
            }
        }
    }

    public void PlayEffectSound(string name)
    {
        _audioSource.PlayOneShot(GetClip(name));
    }

    private AudioClip GetClip(string name)
    {
        AudioClip clip = null;

        if (_audioDictionary.ContainsKey(name))
        {
            clip = _audioDictionary[name];
        }

        return clip;
    }
}
