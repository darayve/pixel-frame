using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FruitsManager : MonoBehaviour
{
    // SerializeFields
    [SerializeField] private TextMeshProUGUI fruitsText;

    // Private
    private static int _fruitsCollected = 0;

    // Public
    public static FruitsManager Instance;
    public static int FruitsCollected
    {
        get => _fruitsCollected;
        set => _fruitsCollected = value;
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
            _fruitsCollected = 0;
            Utils.SavePlayerLives(_fruitsCollected);
        }
        else
        {
            _fruitsCollected = Utils.GetFruitsCounter();
        }
        UpdateFruitsCounter();
    }

    public void UpdateFruitsCounter()
    {
        if (_fruitsCollected == Constants.FRUITS_HEALTH)
        {
            LifeManager.NumberOfLives++;
            LifeManager.Instance.SetLivesCounterText();
            print("Giving one more life!");
        }
        fruitsText.text = _fruitsCollected.ToString();
    }
}
