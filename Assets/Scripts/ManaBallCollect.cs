using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBallCollect : MonoBehaviour
{
    string manaBallName;
    // Start is called before the first frame update
    void Start()
    {
        manaBallName = "ManaBall";
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.name == manaBallName)
        {
            other.gameObject.SetActive(false);
            WorldController.manaBallNum++;
        }
    }
}
