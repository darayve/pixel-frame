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
    private static int _fruitsToEarnHeart = _fruitsCollected;

    // Public
    public static FruitsManager Instance;
    public static int FruitsCollected
    {
        get => _fruitsCollected;
        set => _fruitsCollected = value;
    }

    public static int FruitCounterToHeart
    {
        get => _fruitsToEarnHeart;
        set => _fruitsToEarnHeart = value;
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
        _fruitsToEarnHeart = 0;

        UpdateFruitsCounter();
    }

    public void UpdateFruitsCounter()
    {
        if (_fruitsToEarnHeart == Constants.FRUITS_HEALTH)
        {
            LifeManager.NumberOfLives++;
            LifeManager.Instance.SetLivesCounterText();
            PlayNewLifeSFX();
            _fruitsToEarnHeart = 0;
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

    public void PlayNewLifeSFX()
    {
        SoundManager.Instance.PlaySound(newLifeSFX);
    }
}
