using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 1.0f;

    private int direction = -1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
    }

    // Coroutine for movement
    IEnumerator Move()
    {
        while (true)
        {
            for (float t = 0; t <= 1.5; t += Time.deltaTime)
            {
                transform.Translate(direction * speed * Time.deltaTime, 0, 0);
                yield return null;
            }

            direction *= -1;

        }
    }
}

