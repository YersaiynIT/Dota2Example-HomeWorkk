using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    private AudioHandler _audioHandler;

    private void Awake()
    {
        _audioHandler = new AudioHandler(_audioMixerGroup);
    }

    private void Start()
    {
        _audioHandler.Initialize();
    }

    public void EnableMusic()
    {
        if (_audioHandler.IsMusicOn)
            _audioHandler.OffMusic();
        else
            _audioHandler.OnMusic();
    }

    public void EnableSounds()
    {
        if (_audioHandler.IsSoundsOn)
            _audioHandler.OffSounds();
        else
            _audioHandler.OnSounds();
    }
}
