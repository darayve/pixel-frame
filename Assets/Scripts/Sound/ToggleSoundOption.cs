using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSoundOptions : MonoBehaviour
{
    [SerializeField] private Toggle musicToggle, sfxToggle;

    private void Awake()
    {
        musicToggle.isOn = PlayerPrefs.GetInt(Constants.IS_MUSIC_ON) == 1 ? true : false;
        sfxToggle.isOn = PlayerPrefs.GetInt(Constants.IS_SFX_ON) == 1 ? true : false;
    }
}

