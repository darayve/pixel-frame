using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // SerializeFields
    [SerializeField] private AudioSource musicSource, effectsSource;

    // Private
    private static float _sliderVolume;
    //private static bool _isMusicOn = true, _isSFXOn = true;

    // Public
    public static SoundManager Instance;
    public static float SliderVolume
    {
        get => _sliderVolume;
        set => _sliderVolume = value;
    }

    /*public static bool IsMusicOn
    {
        get => _isMusicOn;
        set => _isMusicOn = value;
    }

    public static bool IsSFXOn
    {
        get => _isSFXOn;
        set => _isSFXOn = value;
    }*/

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        _sliderVolume = volume;
    }

    public void ToggleEffects()
    {
        effectsSource.mute = !effectsSource.mute;
        //_isSFXOn = effectsSource.mute;
        Utils.SaveSFXToggle(isOn: effectsSource.mute);
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        //_isMusicOn = musicSource.mute;
        Utils.SaveMusicToggle(isOn: musicSource.mute);
    }
}
