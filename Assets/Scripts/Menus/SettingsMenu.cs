using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resolutionText;
    [SerializeField] private GameObject otherMenu;
    [SerializeField] private Toggle musicToggle, effectsToggle;

    private static bool _isSettingsMenuActive = false;

    private Resolution[] _resolutions;
    private int currentResolutionIndex = 0;
    private List<string> resOptions = new List<string>();

    public static bool IsSettingsMenuActive => _isSettingsMenuActive;

    private void Awake()
    {
        GetScreenResolutions();

        if (PlayerPrefs.HasKey(Constants.SELECTED_RESOLUTION))
        {
            SelectResolution(PlayerPrefs.GetInt(Constants.SELECTED_RESOLUTION));
            currentResolutionIndex = PlayerPrefs.GetInt(Constants.SELECTED_RESOLUTION);
        }
    }

    private void Update() 
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                otherMenu.SetActive(true);
            }
        }
        _isSettingsMenuActive = gameObject.activeSelf;
    }

    public void SetSoundTogglers()
    {
        musicToggle.isOn = Utils.GetMusicToggleChoice();
        effectsToggle.isOn = Utils.GetSFXToggleChoice();
    }

    public void BackButtonClicked()
    {
        _isSettingsMenuActive = gameObject.activeSelf;
        gameObject.SetActive(false);
        otherMenu.SetActive(true);
    }

    public void GetScreenResolutions()
    {
        _resolutions = Screen.resolutions;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string res = _resolutions[i].width + " x " + _resolutions[i].height;
            resOptions.Add(res);

            if (_resolutions[i].width == Screen.width && _resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionText.text = resOptions[currentResolutionIndex];
    }

    public void SelectResolution(int index)
    {
        Screen.SetResolution(_resolutions[index].width, _resolutions[index].height, true);
        resolutionText.text = resOptions[index];
        Utils.SaveSelectedResolution(resolutionIndex: index);
    }

    public void LeftResolutionButtonPressed()
    {
        currentResolutionIndex--;

        if (currentResolutionIndex == -1)
        {
            currentResolutionIndex = resOptions.Count - 1;
            SelectResolution(currentResolutionIndex);
        } else
        {
            SelectResolution(currentResolutionIndex);
        }
    }

    public void RightResolutionButtonPressed()
    {
        currentResolutionIndex++;

        if (currentResolutionIndex == resOptions.Count)
        {
            currentResolutionIndex = 0;
            SelectResolution(currentResolutionIndex);
        }
        else
        {
            SelectResolution(currentResolutionIndex);
        }
    }
}
