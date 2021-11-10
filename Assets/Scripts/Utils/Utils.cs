using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utils : MonoBehaviour
{
    public static void SaveFruitsCounter(int fruitsCounter)
    {
        PlayerPrefs.SetInt(Constants.FRUITS_COLLECTED, fruitsCounter);
        PlayerPrefs.Save();
    }

    public static int GetFruitsCounter()
    {
        return PlayerPrefs.GetInt(Constants.FRUITS_COLLECTED);
    }

    public static void SavePlayerLives(int lives)
    {
        PlayerPrefs.SetInt(Constants.PLAYER_LIVES, lives);
        PlayerPrefs.Save();
    }
    public static int GetPlayerLives()
    {
        return PlayerPrefs.GetInt(Constants.PLAYER_LIVES);
    }

    public static void SaveSettings(bool isMusicOn, bool isSFXOn)
    {
        PlayerPrefs.SetInt(Constants.IS_MUSIC_ON, isMusicOn ? 1 : 0);
        PlayerPrefs.SetInt(Constants.IS_SFX_ON, isSFXOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static void SaveSelectedResolution(int resolutionIndex)
    {
        PlayerPrefs.SetInt(Constants.SELECTED_RESOLUTION, resolutionIndex);
        PlayerPrefs.Save();

        Debug.Log($"Saving resolution!\nselectedResolution: {PlayerPrefs.GetInt(Constants.SELECTED_RESOLUTION)}");
    }

    public static void SaveVolume(float volume)
    {
        PlayerPrefs.SetFloat(Constants.GAME_VOLUME, volume);
        PlayerPrefs.Save();

        Debug.Log($"Saving volume! gameVolume: {PlayerPrefs.GetFloat(Constants.GAME_VOLUME)}");
    }

    public static void SaveLevel(int level)
    {
        PlayerPrefs.SetInt(Constants.LEVEL, level);
        PlayerPrefs.Save();
    }

    public static void SaveCurrentLevel()
    {
        PlayerPrefs.SetInt(Constants.LEVEL, GetCurrentScene());
        PlayerPrefs.Save();
    }

    public static void SaveMusicToggle(bool isOn)
    {
        PlayerPrefs.SetInt(Constants.IS_MUSIC_ON, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static void SaveSFXToggle(bool isOn)
    {
        PlayerPrefs.SetInt(Constants.IS_SFX_ON, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static bool GetMusicToggleChoice()
    {
        return PlayerPrefs.GetInt(Constants.IS_MUSIC_ON) == 1 ? true : false;
    }

    public static bool GetSFXToggleChoice()
    {
        return PlayerPrefs.GetInt(Constants.IS_SFX_ON) == 1 ? true : false;
    }

    public static int GetNextScene()
    {
        return SceneManager.GetActiveScene().buildIndex + 1;
    }

    public static int GetCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
