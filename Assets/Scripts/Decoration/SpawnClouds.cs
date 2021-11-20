using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnClouds : MonoBehaviour
{
    [SerializeField] private GameObject[] clouds;
    [SerializeField] private float spawnInterval;
    [SerializeField] GameObject end;
    private Vector2 start;
    private void Start()
    {
        start = transform.position;
        PreSpawn();
        StartCoroutine(DoSpawn());
    }

    private void PreSpawn()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector2 spawningPosition = start + Vector2.right * (i * 2);
            Spawn(spawningPosition);
        }
    }

    private void Spawn(Vector2 startPosition)
    {
        int index = Random.Range(0, clouds.Length);
        float randSpeed = Random.Range(1f, 2f);
        float randScale = Random.Range(0.3f, 1.2f);
        float randStartY = Random.Range(start.y - 2f, start.y + 2f);
        GameObject cloud = Instantiate(clouds[index]);
        
        cloud.transform.localScale = new Vector2(randScale, randScale);
        cloud.transform.position = new Vector2(start.x, randStartY);
        Cloud.StartSpawning(randSpeed, end.transform.position.x);
    }

    private IEnumerator DoSpawn()
    {
        yield return new WaitForSeconds(spawnInterval);
        Spawn(start);
        StartCoroutine(DoSpawn());
    }
}
