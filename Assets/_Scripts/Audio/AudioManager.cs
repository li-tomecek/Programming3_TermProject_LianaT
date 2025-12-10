using System.Collections.Generic;
using UnityEngine;
/*
    Simplified version of AudioManager created by Luc Rancourt
*/
public class AudioManager : Singleton<AudioManager>
{
    private AudioSource _musicSource;
    private List<AudioSource> _audioSources = new List<AudioSource>();

    void Awake()
    {   base.Awake();
        _musicSource = GetAudioSource();
    }

    public void SetMusic(SoundEffect music)
    {
        if(_musicSource.isPlaying)
            _musicSource.Stop();
        AssignSource(music, _musicSource);
        _musicSource.Play();
    }

    private void AssignSource(SoundEffect sound, AudioSource source)
    {
        source.clip = sound.Clip;
        source.volume = sound.Volume;
        source.pitch = sound.Pitch;
        source.loop = sound.Loop;
    }

    public AudioSource GetAudioSource()
    {
        foreach(AudioSource source in _audioSources)
        {
            if (source.isPlaying == false)
                return source;
        }

        //if there are no existing sources, add a new one
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();  
        _audioSources.Add(audioSource);
        return audioSource;
    }

    public void PlaySFX(SoundEffect sound)
    {
        AudioSource source = GetAudioSource();
        AssignSource(sound, source);
        source.Play();
    }
}
