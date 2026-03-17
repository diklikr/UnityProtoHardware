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
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].Priority = (i == currentCameraIndex) ? 10 : 0;
        }
    }

    void Update()
    {
        for (int i = 0; i < cameras.Length && i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) ActivateCamera(i);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) SwitchToNextCamera();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) SwitchToPreviousCamera();
    }

    public void ActivateCamera(int index)
    {
        if (index < 0 || index >= cameras.Length || index == currentCameraIndex) return;
        cameras[currentCameraIndex].Priority = 0;
        cameras[index].Priority = 10;
        currentCameraIndex = index;
        StartCoroutine(SlowMotionEffect());
        if (GetComponent<AudioSource>() != null) GetComponent<AudioSource>().Play();
    }

    public void SwitchToNextCamera() { ActivateCamera((currentCameraIndex + 1) % cameras.Length); }
    public void SwitchToPreviousCamera() { ActivateCamera((currentCameraIndex - 1 + cameras.Length) % cameras.Length); }

    public void ZoomIn()
    {
        float fov = cameras[currentCameraIndex].m_Lens.FieldOfView;
        cameras[currentCameraIndex].m_Lens.FieldOfView = Mathf.Clamp(fov - 5f, 20f, 60f);
    }

    public void ZoomOut()
    {
        float fov = cameras[currentCameraIndex].m_Lens.FieldOfView;
        cameras[currentCameraIndex].m_Lens.FieldOfView = Mathf.Clamp(fov + 5f, 20f, 60f);
    }

    IEnumerator SlowMotionEffect()
    {
        Time.timeScale = 0.4f;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1f;
    }
}