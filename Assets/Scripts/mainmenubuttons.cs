using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenubuttons : MonoBehaviour
{
    [SerializeField] private GeneralSounds generalSounds;
    public async void StartGame() 
    {
        await Task.Delay(generalSounds.clickSoundInMs);

        SceneManager.LoadSceneAsync ("Level1");
    }

    public async void LoadGame()
    {
        await Task.Delay(generalSounds.clickSoundInMs);

        SceneManager.LoadSceneAsync ("LoadScene");
    }

    public IEnumerator QuitGame() 
    {
        yield return new WaitForSeconds(generalSounds.clickSound.clip.length);

        Debug.Log ("Quit");
        Application.Quit();
    }
}
