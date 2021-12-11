using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePuzzle : MonoBehaviour
{
    public GameObject puzzle;
    public bool isComplete = false;
    public GameObject characterCamera;
    public GameObject canvas;
    public GameObject effect;
    public Color effectColor;
    private float scale = -1;
    private bool boolPuzzle;

    private void Update()
    {
        if (scale >= 0 && scale < 125)
        {
            effect.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, 1);
            scale += Time.deltaTime * 150;
        }

        else if(scale >= 125)
        {
            effect.SetActive(false);
            scale = -1;
            WorldController.isInPuzzle = true;
            puzzle.SetActive(boolPuzzle);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            characterCamera.SetActive(!boolPuzzle);
            canvas.SetActive(!boolPuzzle);

        }
    }

    public void PuzzleControl(bool state)
    {
        if(!isComplete)
        {
            boolPuzzle = state;
            scale = 0;
            effect.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, 1);
            effect.SetActive(state);
            effect.GetComponent<UnityEngine.UI.RawImage>().color = effectColor;
        }
    }
}
