using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FruitsManager : MonoBehaviour
{
    // SerializeFields
    [SerializeField] private TextMeshProUGUI fruitsText;
    [SerializeField] private AudioClip newLifeSFX;

    // Private
    private static int _fruitsCollected = 0;
    private int fruitsToEarnHeart = _fruitsCollected;

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
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        _fruitsCollected = 0;

        UpdateFruitsCounter();
    }

    public void UpdateFruitsCounter()
    {
        if (fruitsToEarnHeart == _fruitsCollected)
        {
            fruitsToEarnHeart = 0;
        }
        if (_fruitsCollected == Constants.FRUITS_HEALTH)
        {
            LifeManager.NumberOfLives++;
            LifeManager.Instance.SetLivesCounterText();
            SoundManager.Instance.PlaySound(newLifeSFX);
            print("Earned one more life!");
        }
        
        fruitsText.text = "" + _fruitsCollected;
    }

    public void ResetFruitCount()
    {
        _fruitsCollected = 0;
    }

    public void SaveFruits()
    {
        Utils.SaveFruitsCounter(_fruitsCollected);
    }
}
