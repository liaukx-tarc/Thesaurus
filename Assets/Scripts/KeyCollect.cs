using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollect : MonoBehaviour
{
    public void Collect()
    {
        WorldController.keyCollected++;
        Destroy(this.gameObject);
    }
}
