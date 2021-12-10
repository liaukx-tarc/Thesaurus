using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePuzzle : MonoBehaviour
{
    public GameObject puzzle;
    public bool isComplete = false;
    public GameObject characterCamera;
    public GameObject canvas;

    public void PuzzleControl(bool state)
    {
        if(!isComplete)
        {
            WorldController.isInPuzzle = true;
            puzzle.SetActive(state);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            characterCamera.SetActive(!state);
            canvas.SetActive(false);
        }
    }
}
