using System.Collections.Generic;
using UnityEngine;

public class AudioService
{
    private readonly AudioConfiguration audioConfiguration;

    private AudioSource musicSource;

    public AudioService(AudioConfiguration config)
    {
        audioConfiguration = config;
    }

    public void SetGlobalVolume(float value)
    {
        audioConfiguration.SetGlobalVolume(value);
    }

    public void SetMusicVolume(float value)
    {
        audioConfiguration.SetMusicVolume(value);
    }

    public void SetSoundsVolume(float value)
    {
        audioConfiguration.SetSoundsVolume(value);
    }
}