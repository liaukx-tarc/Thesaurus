using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookCollect : MonoBehaviour
{
    public GameObject worldController;
    public bool test;

    private void Update()
    {
        if(test)
        {
            Collect();
            test = false;
        }
    }

    public void Collect()
    {
        WorldController.isEscapeMode = true;
        worldController.GetComponent<WorldController>().IntiEscapePhase();
        Destroy(this.gameObject);
    }
}
