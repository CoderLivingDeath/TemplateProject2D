using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ConfigurationService
{
    private readonly ConfigurationProvider provider;

    public ConfigurationService(ConfigurationProvider provider)
    {
        this.provider = provider;
    }

    public AudioConfiguration GetAudioConfiguration() => provider.AudioConfiguration;
    public InputConfiguration GetInputConfiguration() => provider.InputConfiguration;
}
