using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // SerializeFields
    [SerializeField] private GameObject pauseMenu;

    // Private
    private int mainMenuSceneIndex = 0;
    private bool isGamePaused = false;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isGamePaused && !SettingsMenu.IsSettingsMenuActive)
            {
                Resume();
            } 
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void SaveGame()
    {
        Utils.SaveCurrentLevel();
    }

    public void GoToMainMenu()
    {
        Resume();
        SaveGame();
        SceneManager.LoadScene(mainMenuSceneIndex);
    }

    public void QuitGame()
    {
        Utils.QuitGame();
    }
}
