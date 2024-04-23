using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    
    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            Debug.Log("Move trap");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
