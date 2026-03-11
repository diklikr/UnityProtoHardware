using Cinemachine;
using UnityEngine;

public class CamaraSwitch : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras;
    private int currentCameraIndex = 0;

    void Start()
    {
        // Set initial priorities
        ActivateCamera(0);
    }

    void Update()
    {
        // Switch cameras with number keys 1-7
        if (Input.GetKeyDown(KeyCode.Alpha1)) ActivateCamera(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ActivateCamera(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ActivateCamera(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) ActivateCamera(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) ActivateCamera(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) ActivateCamera(5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) ActivateCamera(6);

        // Or cycle through with arrow keys
        if (Input.GetKeyDown(KeyCode.RightArrow)) SwitchToNextCamera();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) SwitchToPreviousCamera();
    }

    public void ActivateCamera(int index)
    {
        // Deactivate all cameras
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].Priority = 0;
        }

        // Activate the selected camera
        if (index >= 0 && index < cameras.Length)
        {
            cameras[index].Priority = 10;
            currentCameraIndex = index;
        }
    }

    public void SwitchToNextCamera()
    {
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
        ActivateCamera(currentCameraIndex);
    }

    public void SwitchToPreviousCamera()
    {
        currentCameraIndex--;
        if (currentCameraIndex < 0) currentCameraIndex = cameras.Length - 1;
        ActivateCamera(currentCameraIndex);
    }
}
