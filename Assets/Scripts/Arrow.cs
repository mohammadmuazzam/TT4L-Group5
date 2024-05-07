using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow1 : MonoBehaviour
{

[SerializeField]
private float arrowSpeed = 5f;

// Start is called before the first frame update
void MoveTrap()
{
    Rigidbody2D arrow = GetComponent <Rigidbody2D>();
    arrow.velocity = new Vector2(-arrowSpeed, arrow.velocity.y);
}

// Update is called once per frame
void Update()
{
 MoveTrap();   
}
}
