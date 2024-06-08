using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public float speed;
    private Rigidbody2D bulletBody;
    void Awake()
    {
        bulletBody = GetComponent<Rigidbody2D>();
        speed = -10;
    }

    public async void MoveBullet(Vector3 startPos)
    {
        gameObject.transform.position = new Vector3 (startPos.x-1.3f, startPos.y, startPos.z);
        print(gameObject.name);
        while (true)
        {
            try
            {
                print("moving" + gameObject.name);
                bulletBody.velocity = new Vector2(speed, bulletBody.velocity.y);
                await Task.Yield();
            }
            catch (Exception)
            {
                return;
            }
        }   
    }
}
