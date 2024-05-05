using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("");
        Debug.Log("Entering Next Level");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


}

