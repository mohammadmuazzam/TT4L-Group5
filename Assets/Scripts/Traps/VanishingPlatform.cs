using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class VanishingPlatform : MonoBehaviour
{
    [Range(0.1f, 10)][SerializeField] private float timeTaken;

    private Color color;
    private Renderer platformRenderer;

    void Awake()
    {
        platformRenderer = GetComponent<Renderer>();
        color = platformRenderer.material.color;
        gameObject.SetActive(true);
        color.a = 1;
    }

    public async void RemovePlatform()
    {
        try
        {
            float elapsedTime = 0f;
            Color initialColor = color;

            await Task.Delay(2500);

            while (elapsedTime < timeTaken)
            {
                elapsedTime += Time.deltaTime;
                float alphaValue = Mathf.Lerp(initialColor.a, 0f, elapsedTime/timeTaken);
                platformRenderer.material.color = new Color(initialColor.r, initialColor.g, initialColor.b, alphaValue);
                await Task.Yield();
            }
            color.a = 0;
            gameObject.SetActive(false);
        }
        catch (Exception)
        {
            return;
        }
        
    }

    public async void AddPlatform()
    {
        try
        {
            float elapsedTime = 0f;
            Color initialColor = color;

            await Task.Delay(2500);

            while (elapsedTime < timeTaken)
            {
                elapsedTime += Time.deltaTime;
                float alphaValue = Mathf.Lerp(initialColor.a, 0f, elapsedTime/timeTaken);
                platformRenderer.material.color = new Color(initialColor.r, initialColor.g, initialColor.b, alphaValue);
                print("alpha value: " + alphaValue);
                await Task.Yield();
            }
            color.a = 0;
            gameObject.SetActive(false);
        }
        catch(Exception)
        {
            return;
        }
        
    }
}
