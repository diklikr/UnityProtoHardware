using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    float time = 60.0f;
    int minutes = 5;
    public TextMeshPro secs;
    public TextMeshPro mins;
    LoadGame loadGame;

    void Update()
    {
        time -= Time.deltaTime;
        TimeMins();
        mins.text = minutes.ToString("00.");
        secs.text = time.ToString("00.00");
        Death();
    }

    public void ClockButton()
    {

    }

    void TimeMins()
    {
        if (time <= 0)
        {
            minutes--;
            time = 60.0f;
        }
    }

    void Death()
    {
        if (time <= 0)
        {
            loadGame.GameOverScreen();
        }
    }
}
