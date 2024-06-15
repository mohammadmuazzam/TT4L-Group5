using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Button playButton; // Reference to the play button
    public VideoPlayer videoPlayer; // Reference to the video player
    public string level1SceneName = "Level1"; // Name of the Level 1 scene

    public void OnPlayButtonClicked()
    {
        // Hide the Play button
        playButton.gameObject.SetActive(false);

        // Subscribe to the loopPointReached event to know when the video ends
        videoPlayer.loopPointReached += OnVideoEnd;

        // Play the video
        videoPlayer.Play();
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


