using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePuzzle : MonoBehaviour
{
    public GameObject puzzle;
    public bool isComplete = false;
    public GameObject characterCamera;

    public void PuzzleControl(bool state)
    {
        if(!isComplete)
        {
            puzzle.SetActive(state);

            characterCamera.SetActive(!state);
        }
    }
}
