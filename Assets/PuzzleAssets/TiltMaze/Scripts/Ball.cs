using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject LevelClearobj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "goal")
        {
            Destroy(gameObject, .2f);
            LevelClearobj.SetActive(true);
            StartCoroutine(AfterLevelClear());
            Debug.Log("Puzzle Complete!");
        }
    }
       
    IEnumerator AfterLevelClear()
    {
        yield return new WaitForSeconds(3f);
        LevelClearobj.SetActive(false);
    }


}
