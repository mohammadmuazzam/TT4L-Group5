using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    Renderer rockRenderer;
    [SerializeField] private bool isDefaultActive;

    [SerializeField] private float moveAmountX, moveAmountY, moveSpeedX, moveSpeedY, waitTime;

    void Awake()
    {
        rockRenderer = GetComponent<Renderer>();
        gameObject.SetActive(isDefaultActive);
    }

    public IEnumerator MoveAndGrowRock()
    {
        bool hasFinishedMoving = false;
        while (!hasFinishedMoving)
        {
            
        }
        yield return null;
    }

    void ActivateRock()
    {
        gameObject.SetActive(true);
    }
    void DeactivateRock()
    {
        gameObject.SetActive(false);
    }
}
