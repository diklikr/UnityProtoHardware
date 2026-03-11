using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    float time = 60.0f;
    int minutes = 5;
    float timeSub = 15.0f;
    bool pressedButton = true;
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

    public void ClockButton(bool pressed)
    {
        pressedButton = pressed;
        if(time == 40.0f)
        {
            pressedButton = false;
        }
        if (time == 20.0f)
        {
            if (pressedButton == false)
            {
                time -= timeSub;
               pressedButton = true;
            }
        }
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
