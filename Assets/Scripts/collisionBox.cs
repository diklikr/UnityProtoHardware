using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionBox : MonoBehaviour
{
     void OnTriggerEnter(Collider other)
    {
      if(other.gameObject.tag == "Player")
      {
            // abrir pista
        Debug.Log("Player has entered the collision box");
        }
    }


}
