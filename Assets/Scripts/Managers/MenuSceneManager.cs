using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneManager : MonoBehaviour
{
    private static bool _isNewGame;

    public static MenuSceneManager Instance;
    public static bool IsNewGame
    {
        get => _isNewGame;
        set => _isNewGame = value;
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
    }
}
