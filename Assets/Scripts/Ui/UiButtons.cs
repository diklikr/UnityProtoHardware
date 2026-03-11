using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButtons : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _pauseCanvas;

    LoadGame loadGame;
    public void StartButoon()
    {
       loadGame.StartScreen();
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void RestartButton()
    {
       loadGame.GameScreen();
    }

    public void Unpause()
    {
        _pauseCanvas.alpha = 0;
        _pauseCanvas.interactable = false;
        _pauseCanvas.blocksRaycasts = false;
    }

}

