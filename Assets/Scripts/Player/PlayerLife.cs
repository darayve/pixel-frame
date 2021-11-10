using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    // SerializeFields
    [SerializeField] private AudioClip enemyHitSFX;
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] private AudioClip hurtSFX;

    // Private
    private Animator anim;
    private Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.ENEMY_TAG))
        {
            if (PlayerMovement.IsPlayerFalling)
            {
                SoundManager.Instance.PlaySound(enemyHitSFX);
            } else
            {
                SoundManager.Instance.PlaySound(hurtSFX);
            }
        }
        if (collision.gameObject.CompareTag(Constants.TRAP_TAG))
        {
            SoundManager.Instance.PlaySound(hurtSFX);
            Die();
        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        SoundManager.Instance.PlaySound(deathSFX);
    }

    private void RestartLevel()
    {
        // reset life and fruit count
        // save these things
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

