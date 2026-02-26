using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    void LoadScreens(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void StartScreen()
    {
        LoadScreens(0);
    }

    public void GameScreen()
    {
        LoadScreens(1);
    }

    public void GameOverScreen()
        {
            LoadScreens(2);
    }

    public void Victory()
    {
        LoadScreens(3);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
