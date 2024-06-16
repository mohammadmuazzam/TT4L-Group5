using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class Lightning : MonoBehaviour
{
    [SerializeField] private AudioClip[] lightningAudioClip;
    [Range(0,1)] [SerializeField] private float volume;
    private Renderer lightningRenderer;
    private Color color;
    private PolygonCollider2D lightningCollider;
    
    void Awake()
    {
        lightningRenderer = GetComponent<Renderer>();
        lightningCollider = GetComponent<PolygonCollider2D>();
        lightningCollider.enabled = false;
        color = lightningRenderer.material.color;
        color.a = 0;
        lightningRenderer.material.color = color;
    }

    // spawn lightning and play sound
    public async Task SpawnLightning(CancellationTokenSource cancellationTokenSource)
    {
        print("starting lightning");
        _ = SoundFxManager.Instance.PlayRandomSoundFxClipAsync(lightningAudioClip, transform, volume, cancellationTokenSource);
        AddLightning();
        await Task.Delay(2500);
        RemoveLightning();
    }

    private void AddLightning()
    {
        try
        {
            color.a = 1f;
            lightningRenderer.material.color = color;
            lightningCollider.enabled = true;
            print("collider enabled");
            
            print("lightning enabled");
            
        }
        catch (System.Exception)
        {
            return;
        }
        
    }

    private void RemoveLightning()
    {
        try
        {
            color.a = 0;
            lightningRenderer.material.color = color;
            lightningCollider.enabled = false;
        }
        catch(System.Exception)
        {
            return;
        }
    }
}
