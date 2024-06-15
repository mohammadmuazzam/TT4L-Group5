using System.Collections;
using System.Collections.Generic;
using UnityEditor.Scripting;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] float minY, maxY;
    [SerializeField] float moveSpeed = 1.0f;

    bool movingUp = true;

    void Update()
    {
        if (movingUp && transform.position.y < maxY)
        {
            float newY = transform.position.y + moveSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            if (transform.position.y >= maxY)
            {
                movingUp = false;
            }
        }

        if (!movingUp && transform.position.y > minY)
        {
            float newY = transform.position.y - moveSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            if (transform.position.y <= minY)
            {
                movingUp = true;
            }
        }
    }
    public GameManager gameManager;

    // Update is called once per frame
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.coinCounter();

            Destroy (gameObject);
        }
    }
}
