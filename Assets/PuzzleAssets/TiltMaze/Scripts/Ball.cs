using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject LevelClearobj;
    public GameObject puzzleObj;
    bool complete;

    void Start()
    {
        complete = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "goal")
        {
            Destroy(gameObject, .2f);
            LevelClearobj.SetActive(true);
            StartCoroutine(AfterLevelClear());
            Debug.Log("Puzzle Complete!");

            //Tell world controller the puzzle slove
            if(!complete)
            {
                WorldController.puzzleSloved++;
                puzzleObj.SetActive(false);
                WorldController.puzzleComplete = true;
                complete = true;
            }
        }
    }
       
    IEnumerator AfterLevelClear()
    {
        yield return new WaitForSeconds(3f);
        LevelClearobj.SetActive(false);
    }
}
