using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePuzzle : MonoBehaviour
{
    public GameObject puzzle;
    public bool test1, test2;
    public GameObject characterCamera;

    private void Update()
    {
        if(test1)
        {
            PuzzleControl(true);
            test1 = false;
        }

        if (test2)
        {
            PuzzleControl(false);
            test2 = false;
        }
    }

    void PuzzleControl(bool state)
    {
        puzzle.SetActive(state);

        characterCamera.SetActive(!state);
    }
}
