using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow1 : MonoBehaviour
{

[SerializeField]
private float arrowSpeed = 5f;

[SerializeField]
private float uTurnPosition = -5f;

private Rigidbody2D arrow;
private bool hasRotated = false;
private float tempZRotation;


private void Start()
{
    arrow = GetComponent <Rigidbody2D>();
}
private void MoveTrap()
{
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
            tempZRotation += 90f;
       
            if (tempZRotation >= 180f)
                hasRotated = true;  
                transform.Rotate (0f,0f,tempZRotation); 
        } 
    }
    
    else
    {
        arrow.velocity = new Vector2(arrowSpeed,arrow.velocity.y);
    }

}
// Update is called once per frame
private void Update()
{
    MoveTrap(); 
}
}


