using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    // SerializeFields
    [SerializeField] private AudioClip finishSFX;

    // Private
    private bool levelCompleted = false;
    private int nextLevel;

    private void Start()
    {
        nextLevel = Utils.GetNextScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.PLAYER_TAG) && !levelCompleted)
        {
            SoundManager.Instance.PlaySound(finishSFX);
            levelCompleted = true;
            Invoke("CompleteLevel", 1.5f);
        }
    }

    private void CompleteLevel()
    {
        Utils.SaveLevel(nextLevel);
        Utils.SavePlayerLives(LifeManager.NumberOfLives);
        Utils.SaveFruitsCounter(FruitsManager.FruitsCollected);
        PlayerPrefs.Save();
        SceneManager.LoadScene(nextLevel);
    }
}
