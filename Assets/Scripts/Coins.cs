using System.Collections;
using System.Collections.Generic;
using UnityEditor.Scripting;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] float floatDistance;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] AudioClip coinsCollectSound;
    [Range(0, 1)] [SerializeField] float volume;

    bool movingUp = true;
    float initialYPos;

    void Awake()
    {
        initialYPos = transform.position.y;
    }

    void Update()
    {
        if (movingUp && transform.position.y < initialYPos+floatDistance)
        {
            float newY = transform.position.y + moveSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            if (transform.position.y >= initialYPos+floatDistance)
            {
                movingUp = false;
            }
        }

        if (!movingUp && transform.position.y > initialYPos-floatDistance)
        {
            float newY = transform.position.y - moveSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            if (transform.position.y <= initialYPos-floatDistance)
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
            SoundFxManager.Instance.PlaySoundFxClip(coinsCollectSound, transform, volume);

            Destroy (gameObject);
        }
    }
}
