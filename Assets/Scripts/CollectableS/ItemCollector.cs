using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    // SerializeFields
    [SerializeField] private AudioClip collectSFX;

    // Private
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
            FruitsManager.Instance.UpdateFruitsCounter();
            SoundManager.Instance.PlaySound(collectSFX);
        }
    }
}
