using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEndGame : MonoBehaviour
{
    static public int manaballNeeded;
    private void OnTriggerEnter(Collider other)
    {
        if (WorldController.manaBallNum >= manaballNeeded)
        {
            if (other.tag == "Player")
            {
                WorldController.isWin = true;
            }
        }
    }
}
