using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Lumin;

public class Laser : MonoBehaviour
{
    [SerializeField] private AudioClip[] laserShootFx;
    [Range(0, 1)] [SerializeField] private float laserVolume;
    private GameObject laserObject;
    public BoxCollider2D laserCollider;

    private float initialXScale, finalXScale, tempXScale;
    void Awake()
    {
        laserObject = GameObject.Find("Laser");
        laserCollider = laserObject.GetComponent<BoxCollider2D>();
        laserCollider.enabled = false;

        initialXScale = 0.1f;
        finalXScale = 200f;

        // hide laser
        gameObject.transform.localScale = new Vector3(initialXScale, 6f, 1);

    }

    public async Task ShootLaser(CancellationTokenSource cancellationTokenSource)
    {
        try
        {
            float elapsedTime = 0f;
            _ = SoundFxManager.Instance.PlayRandomSoundFxClipAsync(laserShootFx, transform, laserVolume, cancellationTokenSource);

            //* shoot laser
            while (elapsedTime < 0.7f)
            {
                //* move temp values
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= 0.25f)
                {
                    laserCollider.enabled = true;
                }
                tempXScale = Mathf.Lerp(initialXScale, finalXScale, elapsedTime/0.5f);

                //* reset laser if player dies
                if (Player.isPlayerAlive == false || cancellationTokenSource.IsCancellationRequested)
                {
                    gameObject.transform.localScale = new Vector3(initialXScale, 6f, 0.5f);
                    return;
                }

                //* assign temp values to object
                gameObject.transform.localScale = new Vector3(tempXScale, 6f, 1);
                await Task.Yield();
            }
            gameObject.transform.localScale = new Vector3(finalXScale, 6f, 1);
            await Task.Delay(500);
            elapsedTime = 0;

            //* unshoot laser
            while (elapsedTime < 0.7)
            {
                elapsedTime += Time.deltaTime;
                tempXScale = Mathf.Lerp(finalXScale, initialXScale, elapsedTime/0.5f);
                if (elapsedTime >= 0.25)
                {
                    laserCollider.enabled = false;
                }

                //* reset laser if player dies
                if (Player.isPlayerAlive == false  || cancellationTokenSource.IsCancellationRequested)
                {
                    gameObject.transform.localScale = new Vector3(initialXScale, 6f, 1);
                    return;
                }

                //* assign temp values to object
                gameObject.transform.localScale = new Vector3(tempXScale, 6f, 1);
                await Task.Yield();
            }
            gameObject.transform.localScale = new Vector3(initialXScale, 6f, 1);
            await Task.Delay(300);
        }
        catch (System.Exception)
        {
            return;
        }
    }
}
