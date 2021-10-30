using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Awake()
    {
        slider.value = PlayerPrefs.GetFloat(Constants.GAME_VOLUME);
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(value => {
            SoundManager.Instance.ChangeMasterVolume(value);
            Utils.SaveVolume(volume: value);
        });
    }
}
