using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ball : MonoBehaviour
{
    public GameObject puzzleObj;

    public RawImage LevelClear;
    public GameObject Confetti;
    public GameObject Sphere;
    TimerScript timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {

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
        Debug.Log("Complete");
        WorldController.puzzleSloved++;
        WorldController.puzzleComplete = true;
        puzzleObj.SetActive(false);
    }
}
