using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePuzzle : MonoBehaviour
{
    public GameObject puzzle;
    public bool test1;
    public GameObject characterCamera;

    private void Update()
    {
        if(test1)
        {
            PuzzleControl(true);
            Debug.Log("Open");
            test1 = false;
        }
    }
    public void PuzzleControl(bool state)
    {
        puzzle.SetActive(state);

        characterCamera.SetActive(!state);
    }
}
