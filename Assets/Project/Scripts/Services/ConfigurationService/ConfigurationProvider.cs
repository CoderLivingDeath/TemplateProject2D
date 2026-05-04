using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationProvider : MonoBehaviour
{
    [SerializeField]
    private AudioConfiguration _audioConfiguration;
    [SerializeField]
    private InputConfiguration _inputConfiuguration;

    [SerializeField] private GameObject _draftObject;

    public GameObject DraftObject => _draftObject;

    public AudioConfiguration AudioConfiguration => _audioConfiguration;
    public InputConfiguration InputConfiguration => _inputConfiuguration;

}
