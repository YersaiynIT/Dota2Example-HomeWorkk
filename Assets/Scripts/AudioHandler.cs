using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler
{
    private const float OnVolume = 0f;
    private const float OffVolume = -80f;

    private const string MusicKey = "MusicVolume";
    private const string SoundsKey = "SoundsVolume";

    private const float OnVolumeSaveKey = 1;
    private const float OffVolumeSaveKey = -1;

    private AudioMixerGroup _masterGroup;

    public AudioHandler(AudioMixerGroup masterGroup)
    {
        _masterGroup = masterGroup;
    }

    public void Initialize()
    {
        float musicSaveKey = PlayerPrefs.GetInt(MusicKey);

        if (musicSaveKey == 0 || musicSaveKey == OnVolumeSaveKey)
            OnMusic();
        else
            OffMusic();

        float soundsSaveKey = PlayerPrefs.GetInt(SoundsKey);

        if (soundsSaveKey == 0 || soundsSaveKey == OnVolumeSaveKey)
            OnSounds();
        else
            OffSounds();
    }

    public bool IsMusicOn => PlayerPrefs.GetFloat(MusicKey) == OnVolumeSaveKey;

    public bool IsSoundsOn => PlayerPrefs.GetFloat(SoundsKey) == OnVolumeSaveKey;

    public void OnMusic()
    {
        _masterGroup.audioMixer.SetFloat(MusicKey, OnVolume);
        PlayerPrefs.SetFloat(MusicKey, OnVolumeSaveKey);
    }

    public void OffMusic()
    {
        _masterGroup.audioMixer.SetFloat(MusicKey, OffVolume);
        PlayerPrefs.SetFloat(MusicKey, OffVolumeSaveKey);
    }

    public void OnSounds()
    {
        _masterGroup.audioMixer.SetFloat(SoundsKey, OnVolume);
        PlayerPrefs.SetFloat(SoundsKey, OnVolumeSaveKey);
    }

    public void OffSounds()
    {
        _masterGroup.audioMixer.SetFloat(SoundsKey, OffVolume);
        PlayerPrefs.SetFloat(SoundsKey, OffVolumeSaveKey);
    }
}
