using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    // Start is called before the first frame update
    Renderer lightningRenderer;
    Color color;
    private AudioSource audioSource;
    void Awake()
    {
        lightningRenderer = GetComponent<Renderer>();
        color = lightningRenderer.material.color;
        color.a = 0;
        lightningRenderer.material.color = color;
        gameObject.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    // spawn lightning and play sound
    public IEnumerator SpawnLightning()
    {
        AddLightning();
        print("audio source: " + audioSource); 
        audioSource.Play();
        yield return new WaitForSeconds(2f);
        RemoveLightning();
    }

    private void AddLightning()
    {
        while (color.a < 1)
        {
            color.a += 0.01f;
            lightningRenderer.material.color = color;
        }
        gameObject.SetActive(true);
    }

    private void RemoveLightning()
    {
        while (color.a > 0)
        {
            color.a -= 0.01f;
            lightningRenderer.material.color = color;
            print("color.a: " + color.a);
        }
        gameObject.SetActive(false);
    }
}
