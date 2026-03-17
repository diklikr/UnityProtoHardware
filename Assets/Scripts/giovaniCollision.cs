using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giovaniCollision : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
          canvasGroup.alpha = 1f;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
          canvasGroup.alpha = 0f;
        }
    }
}

