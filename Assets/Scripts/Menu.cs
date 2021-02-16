using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string nextLevel;

    /*public void Replay()
    {
        FindObjectOfType<GameManager>().Reset();
    }*/

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
