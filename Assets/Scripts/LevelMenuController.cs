using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour
{
    [SerializeField] private GeneralSounds generalSounds;
    [SerializeField] Button[] buttons;
    public async void BackToMainMenu() 
    {
        await Task.Delay(generalSounds.clickSoundInMs);

        SceneManager.LoadSceneAsync ("MainMenu");
    }

    private void Awake()
    {
        int lockLevel = PlayerPrefs.GetInt ("LockLevel",1);
        for (int i=0; i < buttons.Length; i++)
        {
            buttons [i].interactable = false;
        }

        for (int i=0; i < lockLevel; i++)
        {
            buttons [i].interactable = true;
        }
    }

    public void LevelOpen(int levelId)
    {
        string levelName = "Level" + levelId;
        SceneManager.LoadScene (levelName);
    }
}
