using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    // SerializeFields
    [SerializeField] private TextMeshProUGUI orangesText;
    [SerializeField] private AudioClip collectSFX;

    // Private
    private int oranges = 0;
    private Animator fruitAnim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        fruitAnim = collision.GetComponent<Animator>();

        if (collision.gameObject.CompareTag(Constants.COLLECTABLE_TAG))
        {
            fruitAnim.SetTrigger("collected");
            Destroy(collision.gameObject, collision.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            collision.enabled = false;
            oranges++;
            orangesText.text = "" + oranges;
            SoundManager.Instance.PlaySound(collectSFX);
        }
    }
}
