using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBox : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canvasGroup.alpha = 1f;
             canvasGroup.interactable = true;
             canvasGroup.blocksRaycasts = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
       canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

}
