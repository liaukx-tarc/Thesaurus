using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollect : MonoBehaviour
{
    public bool test;

    // Update is called once per frame
    void Update()
    {
        if(test)
        {
            Collect();
            test = false;
        }
    }

    public void Collect()
    {
        WorldController.keyCollected++;
        Destroy(this.gameObject);
    }
}
