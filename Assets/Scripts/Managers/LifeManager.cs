using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeManager : MonoBehaviour
{
    // SerializeFields
    [SerializeField] private TextMeshProUGUI lifeText;

    // Private
    private static int _numberOfLives = Constants.NUMBER_OF_LIVES;

    // Public 
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
            Destroy(gameObject);
        }

        if (MenuSceneManager.IsNewGame)
        {
            _numberOfLives = Constants.NUMBER_OF_LIVES;
            Utils.SavePlayerLives(_numberOfLives);
        }
        else
        {
            _numberOfLives = Utils.GetPlayerLives();
        }
        SetLivesCounterText();
    }

    public void SetLivesCounterText()
    {
        lifeText.text = _numberOfLives.ToString();
    }
}
