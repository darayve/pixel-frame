using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private AudioClip collectSFX;

    private Animator fruitAnim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        fruitAnim = collision.GetComponent<Animator>();

        if (collision.gameObject.CompareTag(Constants.COLLECTABLE_TAG))
        {
            fruitAnim.SetTrigger("collected");
            Destroy(collision.gameObject, collision.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            collision.enabled = false;
            FruitsManager.FruitsCollected++;
            FruitsManager.FruitCounterToHeart++;
            FruitsManager.Instance.UpdateFruitsCounter();
            SoundManager.Instance.PlaySound(collectSFX);
            if (collision.gameObject.GetComponent<CollectableScript>().itemType == Constants.ITEM_HEALTH)
            {
                CollectFruitHealth();
            }
        }
    }

    private void CollectFruitHealth()
    {
        LifeManager.NumberOfLives++;
        LifeManager.Instance.SetLivesCounterText();
        FruitsManager.Instance.PlayNewLifeSFX();
    }
}
