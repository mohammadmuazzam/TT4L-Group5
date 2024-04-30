using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public void StartGame() {

        Debug.Log ("Button Function");
        SceneManager.LoadScene ("Level1");
    }

    public void QuitGame() {

        Debug.Log ("Quit");
        Application.Quit();
    }
}
