using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow1 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private float arrowSpeed = 5f;
    [SerializeField] private float uTurnPosition = -5f;
    [SerializeField] private AudioClip arrowSound;
    [Range(0, 1)] [SerializeField] private float maxVolume;

    private AudioSource audioSource;
    private Rigidbody2D arrow;
    private bool hasRotated, hasStartedAudio = false;
    private float tempZRotation;

    private void Start()
    {
        arrow = GetComponent <Rigidbody2D>();
        audioSource = GetComponent <AudioSource>();
    }
    private void MoveTrap()
    {
        if (!hasStartedAudio)
        {
            //SoundFxManager.Instance.PlaySoundFxClip(arrowSound, transform, volume);
            hasStartedAudio = true;
        }
        
        if (!hasRotated)
        {
            if (transform.position.x > uTurnPosition)
            {
                arrow.velocity = new Vector2(-arrowSpeed,arrow.velocity.y);
            }
            else
            {
                arrow.velocity = Vector2.zero;
                tempZRotation = transform.rotation.z;
                tempZRotation += 180f;

                if (transform.rotation.z <= 180f)
                {
                    transform.Rotate (0f,0f,tempZRotation);
                    hasRotated = true;
                }
                Debug.Log(transform.rotation.z);

            }
        }
        else
        {
            arrow.velocity = new Vector2(arrowSpeed,arrow.velocity.y);
        }

    }

    void AdjustVolume()
    {
        float distance = (float) Math.Sqrt((transform.position.x - player.transform.position.x)*(transform.position.x - player.transform.position.x) + (transform.position.y - player.transform.position.y)*(transform.position.y - player.transform.position.y));
        if (distance <= 1)
        {
            audioSource.volume = maxVolume;
        }
        else if (distance > 100)
        {
            audioSource.volume = 0f;
        }
        else if (distance > 10)
        {
            audioSource.volume = maxVolume - (maxVolume*(distance*distance/1000));
        }
        else
        {
            audioSource.volume = maxVolume - (maxVolume*(distance-10)*(distance-10)/1000);
        }
        //print("volume: " + audioSource.volume);
    }

    private void Update()
    {
        MoveTrap();
        AdjustVolume();
    }
}


