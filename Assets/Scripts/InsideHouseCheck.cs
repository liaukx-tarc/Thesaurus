using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideHouseCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController.isInsideHouse = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.isInsideHouse = false;
        }
    }
}
