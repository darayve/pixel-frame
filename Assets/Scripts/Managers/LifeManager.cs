using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lifeText;

    private static int _numberOfLives = Constants.NUMBER_OF_LIVES;

    public static LifeManager Instance;
    public static int NumberOfLives
    {
        get => _numberOfLives;
        set => _numberOfLives = value;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Destroy(gameObject);
        }
        
        if (MenuSceneManager.IsNewGame)
        {
            _numberOfLives = Constants.NUMBER_OF_LIVES;
        } else
        {
            _numberOfLives = Utils.GetPlayerLives();
        }

        SetLivesCounterText();
    }

    public void SetLivesCounterText()
    {
        lifeText.text = "" + _numberOfLives;
    }

    public void ResetLives()
    {
        _numberOfLives = Constants.NUMBER_OF_LIVES;
    }

    public void SaveLives()
    {
        Utils.SavePlayerLives(_numberOfLives);
    }
}
