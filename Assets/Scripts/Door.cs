using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject door;

    public void OpenDoor()
    {
        door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + 5.0f, door.transform.position.z);
    }

}
