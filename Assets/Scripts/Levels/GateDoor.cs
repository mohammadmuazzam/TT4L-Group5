using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Threading;

public class GateDoor : MonoBehaviour
{
    public bool playerReachesGate;
    [SerializeField] AudioClip winningAudioClip;
    [Range(0, 1)] [SerializeField] float volume;
    public LevelMenuController[] levelMenuControllers;

    // delegate & events
    public delegate void PlayerWins();
    public static event PlayerWins playerWins;
    private CancellationTokenSource cancellationTokenSource;

    void Awake()
    {
        playerReachesGate = false;
        cancellationTokenSource = new CancellationTokenSource();
    }
    async void OnTriggerEnter2D (Collider2D player)
    {
        UnlockNewLevel();
        if (player.gameObject.CompareTag("Player"))
        {
            playerReachesGate = true;
            Debug.Log("player reach gate");
            if (playerWins != null)
            {
                Player.canPlayerMove = false;
                await SoundFxManager.Instance.PlaySoundFxClipAsync(winningAudioClip, transform, volume, cancellationTokenSource);
                playerWins();
            }
        }
    }

    public void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("EndPoint"))
        {
            PlayerPrefs.SetInt("EndPoint", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("LockLevel",PlayerPrefs.GetInt("LockLevel",1) + 1);
            PlayerPrefs.Save();
        }
    }

    void OnApplicationQuit()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
