using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource, effectsSource;

    private static float _sliderVolume;

    public static SoundManager Instance;
    public static float SliderVolume
    {
        get => _sliderVolume;
        set => _sliderVolume = value;
    }

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
        Utils.SaveSFXToggle(isOn: effectsSource.mute);
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        Utils.SaveMusicToggle(isOn: musicSource.mute);
    }
}
