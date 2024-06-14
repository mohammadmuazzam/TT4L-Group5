using System.Collections;
using System.Collections.Generic;
using UnityEditor.Scripting;
using UnityEngine;

public class Coins : MonoBehaviour
{
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
