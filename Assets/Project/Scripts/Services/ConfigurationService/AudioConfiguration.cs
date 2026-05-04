using System;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioConfiguaration", menuName = "configs/AudioConfiguaration")]
public class AudioConfiguration : ScriptableObject
{
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private string _GlobalAudioVolumeVariableKey = "GlobalVolume";

    [SerializeField]
    private string _MusicAudioVolumeVariableKey = "MusicVolume";

    [SerializeField]
    private string _SoundAudioVolumeVariableKey = "SoundVolume";

    public string GlobalAudioVolumeVariableKey => _GlobalAudioVolumeVariableKey;

    public string MusicAudioVolumeVariableKey => _MusicAudioVolumeVariableKey;

    public string SoundAudioVolumeVariableKey => _SoundAudioVolumeVariableKey;

    public void SetGlobalVolume(float value)
    {
        throw new NotImplementedException();
    }

    public void SetMusicVolume(float value)
    {
        throw new NotImplementedException();
    }

    public void SetSoundsVolume(float value)
    {
        throw new NotImplementedException();
    }
}
