using UnityEngine;

public class ItemCollected : MonoBehaviour
{
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
