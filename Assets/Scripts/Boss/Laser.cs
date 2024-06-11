using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Lumin;

public class Laser : MonoBehaviour
{
    [SerializeField] private AudioClip[] laserShootFx;
    [Range(0, 1)] [SerializeField] private float laserVolume;
    private GameObject laserObject;
    private BoxCollider2D laserCollider;

    private float initialXScale, finalXScale, tempXScale;
    void Awake()
    {
        laserObject = GameObject.Find("Laser");
        laserCollider = laserObject.GetComponent<BoxCollider2D>();
        laserCollider.enabled = false;

        initialXScale = 0.1f;
        finalXScale = 400f;

        // hide laser
        gameObject.transform.localScale = new Vector3(initialXScale, 6f, 1);

        print("initialized laser's mask");
    }

    public async void ShootLaser()
    {
        try
        {
            print("shoot laser is laser");
            float elapsedTime = 0f;
            SoundFxManager.Instance.PlayRandomSoundFxClip(laserShootFx, transform, laserVolume);

            //* shoot laser
            while (elapsedTime < 2.3)
            {
                //* move temp values
                elapsedTime += Time.deltaTime;
                tempXScale = Mathf.Lerp(initialXScale, finalXScale, elapsedTime/2.3f);

                //* assign temp values to object
                gameObject.transform.localScale = new Vector3(tempXScale, 6f, 1);
                await Task.Yield();
            }
            gameObject.transform.localScale = new Vector3(finalXScale, 6f, 1);
            laserCollider.enabled = true;

            //* unshoot laser
            while (elapsedTime < 2.3)
            {
                elapsedTime += Time.deltaTime;
                tempXScale = Mathf.Lerp(finalXScale, initialXScale, elapsedTime/2.3f);
                
                //* assign temp values to object
                gameObject.transform.localScale = new Vector3(tempXScale, 6f, 1);
                await Task.Yield();
            }
            gameObject.transform.localScale = new Vector3(initialXScale, 6f, 1);
            laserCollider.enabled = true;

        }
        catch (System.Exception)
        {
            return;
        }

    }

    
}
