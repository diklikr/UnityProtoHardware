using Cinemachine;
using System.Collections;
using UnityEngine;

public class CamaraSwitch : MonoBehaviour
{
    [Header("Configuración de Cámaras")]
    public CinemachineVirtualCamera[] cameras;

    [SerializeField] private int currentCameraIndex = 0;

    void Start()
    {
        // Inicializamos: todas a 0 excepto la primera
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].Priority = (i == currentCameraIndex) ? 10 : 0;
        }
    }

    void Update()
    {
        // 1. Detección genérica de números (Alpha1 al Alpha9)
        for (int i = 0; i < cameras.Length && i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                ActivateCamera(i);
            }
        }

        // 2. Ciclo con flechas
        if (Input.GetKeyDown(KeyCode.RightArrow)) SwitchToNextCamera();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) SwitchToPreviousCamera();
    }

    public void ActivateCamera(int index)
    {
        // Validación de rango
        if (index < 0 || index >= cameras.Length || index == currentCameraIndex) return;

        // APAGAMOS la actual, ENCENDEMOS la nueva (Solo 2 operaciones en vez de un bucle for)
        cameras[currentCameraIndex].Priority = 0;
        cameras[index].Priority = 10;

        currentCameraIndex = index;
        StartCoroutine(SlowMotionEffect());
        GetComponent<AudioSource>().Play(); // Reproducir sonido de cambio de cámara
    }

    public void SwitchToNextCamera()
    {
        int next = (currentCameraIndex + 1) % cameras.Length;
        ActivateCamera(next);
    }

    public void SwitchToPreviousCamera()
    {
        int prev = (currentCameraIndex - 1 + cameras.Length) % cameras.Length;
        ActivateCamera(prev);
    }

    IEnumerator SlowMotionEffect()
    {
        Time.timeScale = 0.4f; // 40% de la velocidad normal
        yield return new WaitForSecondsRealtime(0.2f); // Espera tiempo real (no afectado por la escala)
        Time.timeScale = 1f; // Vuelve a la normalidad
    }
}