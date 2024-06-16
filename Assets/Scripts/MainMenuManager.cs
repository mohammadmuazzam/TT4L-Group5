using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Button playButton;
    [SerializeField] Button[] buttons;
    public VideoPlayer videoPlayer; // Reference to the video player
    public string level1SceneName = "Level1"; // Name of the Level 1 scene

    public void OnPlayButtonClicked()
    {
        print("lock level: " + PlayerPrefs.GetInt("LockLevel"));

        //* play video on new game
        if (PlayerPrefs.GetInt("LockLevel") == 0)
        {
            // disable buttons
            playButton.gameObject.SetActive(false);
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }

            // Subscribe to the loopPointReached event to know when the video ends
            videoPlayer.loopPointReached += OnVideoEnd;

            videoPlayer.Play();
        }
        else
        {
            string levelName = "Level" + PlayerPrefs.GetInt("LockLevel").ToString();
            SceneManager.LoadScene(levelName);
        }
        
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        print("video ended");
        // Unsubscribe from the event
        videoPlayer.loopPointReached -= OnVideoEnd;

        // Load the Level 1 scene
        SceneManager.LoadScene(level1SceneName);
    }
}


