using UnityEngine;

public class ItemCollected : MonoBehaviour
{
    // Private
    private Collider2D coll;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnDestroy()
    {
        coll.enabled = true;
    }
}
