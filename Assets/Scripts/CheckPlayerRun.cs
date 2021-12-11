using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerRun : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(WorldController.isEscapeMode)
        {
            if (other.tag == "Player")
            {
                WorldController.isPlayerOut = true;
                DoorControl.isLock = true;
            }
        }
    }
}
