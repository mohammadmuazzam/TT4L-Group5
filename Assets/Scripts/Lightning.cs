using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    // Start is called before the first frame update
    Renderer ligntningRenderer;
    Color color;
    void Awake()
    {
        ligntningRenderer = GetComponent<Renderer>();
        color = ligntningRenderer.material.color;
        print($"color: {color}");
    }

    public IEnumerator SpawnLightning()
    {
        while (color.a >= 0)
        {
            print("minusing color");
            color.a = color.a - 0.01f;
        }
        yield return null;
    }
}
