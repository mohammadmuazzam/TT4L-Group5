using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow1 : MonoBehaviour
{

[SerializeField]
private float arrowSpeed = 5f;

[SerializeField]private float uTurnPosition = -5f;

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
// Update is called once per frame
private void Update()
{
    MoveTrap(); 

}
}


