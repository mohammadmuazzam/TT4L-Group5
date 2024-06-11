using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerStay2D (Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
