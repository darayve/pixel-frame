using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private static float _speed;
    private static float _endPositionX;

    private void Update()
    {
        transform.Translate(Vector2.right * (Time.deltaTime * _speed));
        if (transform.position.x > _endPositionX)
        {
            Destroy(gameObject);
        }
    }

    public static void StartSpawning(float speed, float endPositionX)
    {
        _speed = speed;
        _endPositionX = endPositionX;
    }
}
