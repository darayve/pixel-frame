using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        //LoadVolume();
        //LoadToggleAudioOptions();
        //PlayerPrefs.DeleteAll();
    }

    public void NewGame()
    {
        MenuSceneManager.IsNewGame = true;
        Utils.SaveLevel(level: 1);
        SceneManager.LoadScene(Utils.GetNextScene());
    }

    public void Continue()
    {
        if (PlayerPrefs.HasKey(Constants.LEVEL))
        {
            MenuSceneManager.IsNewGame = false;
            SceneManager.LoadScene(PlayerPrefs.GetInt(Constants.LEVEL));
        } else
        {
            Debug.Log("Não foi encontrado um jogo salvo! Iniciando novo jogo.");
            NewGame();
        }
    }

    public static void QuitGame()
    {
        Utils.QuitGame();
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey(Constants.GAME_VOLUME))
        {
            SoundManager.SliderVolume = PlayerPrefs.GetFloat(Constants.GAME_VOLUME);
            SoundManager.Instance.ChangeMasterVolume(PlayerPrefs.GetFloat(Constants.GAME_VOLUME));
        }
        else
        {
            SoundManager.Instance.ChangeMasterVolume(SoundManager.SliderVolume);
        }
    }

    private void LoadToggleAudioOptions()
    {
        if (PlayerPrefs.HasKey(Constants.IS_MUSIC_ON) || PlayerPrefs.HasKey(Constants.IS_SFX_ON))
        {
            //SettingsMenu.SetSetSoundTogglers();
        } else
        {
            Debug.Log("Default values for isMusicOn and isSFXOn were applied.");
        }
    }
}
