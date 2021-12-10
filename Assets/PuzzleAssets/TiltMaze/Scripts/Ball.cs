using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ball : MonoBehaviour
{
    public GameObject puzzleObj;
    public GameObject crystal;
    public RawImage LevelClear;
    public GameObject Confetti;
    public GameObject Sphere;
    TimerScript timer;

    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if(Input.GetButtonDown("Escape"))
        {
            if (WorldController.puzzleUnlock)
                puzzleObj.SetActive(false);
        }
    }

    public IEnumerator OnTriggerEnter(Collider gameObject)
    {
        if (gameObject.name == "goal")
        {
            LevelClear.gameObject.SetActive(true);
            Confetti.SetActive(true);
            StartCoroutine(AfterLevelClear());
            timer = FindObjectOfType<TimerScript>();
            timer.StopTimer();
        }

        yield return null;
    }

    IEnumerator AfterLevelClear()
    {
        yield return new WaitForSeconds(2.0f);
        LevelClear.gameObject.SetActive(false);

        //Tell the world controller the puzzle sloved
        crystal.GetComponent<ActivePuzzle>().isComplete = true;
        for (int i = 0; i < crystal.transform.childCount; i++)
        {
            crystal.transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.1f, 0.1f, 0.1f));
        }
        Material[] materials = new Material[1];
        materials[0] = crystal.transform.GetChild(1).GetComponent<MeshRenderer>().materials[0];
        crystal.transform.GetChild(1).GetComponent<MeshRenderer>().materials = materials;
        WorldController.puzzleSloved++;
        WorldController.puzzleComplete = true;
        puzzleObj.SetActive(false);
    }
}
