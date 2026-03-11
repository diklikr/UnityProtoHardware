using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryBox : MonoBehaviour
{
    LoadGame loadGame;

        void OnTriggerEnter(Collider other)
        {
        if(other.gameObject.tag == "Player")
        {
            loadGame.Victory();
            }
    }
}
