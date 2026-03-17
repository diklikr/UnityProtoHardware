using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [Header("Configuración de Tiempo")]
    float time = 60.0f;
    public int minutes = 4;
    float timeSub = 15.0f;

    [Header("Referencias de UI y Game Over")]
    public TextMeshPro secs;
    public TextMeshPro mins;

    // IMPORTANTE: Ahora es pública para que la puedas arrastrar desde el Inspector
    public LoadGame loadGame;

    bool pressedButton = true;
    bool isGameOver = false; // Bandera para detener el reloj cuando perdemos

    void Start()
    {
        // Medida de seguridad: Si olvidan arrastrar el script LoadGame en el inspector, Unity lo busca automático
        if (loadGame == null)
        {
            loadGame = FindObjectOfType<LoadGame>();
        }
    }

    void Update()
    {
        // Si el juego ya terminó, el reloj deja de correr
        if (isGameOver) return;

        time -= Time.deltaTime;

        // Verificamos si los segundos llegaron a cero
        if (time <= 0)
        {
            minutes--; // Restamos un minuto

            if (minutes < 0)
            {
                // Si los minutos bajan de 0, se acabó el tiempo
                time = 0;
                minutes = 0;
                Death();
            }
            else
            {
                // Si aún hay minutos, reiniciamos los segundos
                time = 59.99f;
            }
        }

        // Actualizamos los textos en pantalla
        mins.text = minutes.ToString("00");
        // Usamos Mathf.FloorToInt para redondear hacia abajo y evitar que muestre decimales o "60"
        secs.text = Mathf.FloorToInt(time).ToString("00");
    }

    // Corregí la lógica de los botones usando rangos lógicos en lugar de valores exactos
    public void ClockButton(bool pressed)
    {
        pressedButton = pressed;

        // Si estamos entre el segundo 40 y 39
        if (time <= 40.0f && time > 39.0f)
        {
            pressedButton = false;
        }

        // Si estamos entre el segundo 20 y 19
        if (time <= 20.0f && time > 19.0f)
        {
            if (pressedButton == false)
            {
                time -= timeSub;
                pressedButton = true;
            }
        }
    }

    void Death()
    {
        isGameOver = true; // Detiene el reloj

        if (loadGame != null)
        {
            loadGame.GameOverScreen(); // Llama a la pantalla de Game Over
        }
        else
        {
            Debug.LogError("Error: El script LoadGame no está asignado ni se encontró en la escena.");
        }
    }
}