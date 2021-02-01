using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string lvlToLoad;

    public void PlayGame()
    {
        SceneManager.LoadScene(lvlToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
