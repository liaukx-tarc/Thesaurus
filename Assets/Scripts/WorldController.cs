using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public float cutSceneDuration;
    public GameObject characterCamera;
    bool isEscapeMode;

    //Explore Area
    static public int keyCollected;
    public int keyNum;
    bool exploreAreaClear;
    public GameObject[] exploreAreaDoor;
    public GameObject exploreAreaCamera;

    //Puzzle Area
    static public int puzzleSloved;
    static public bool puzzleComplete;
    public int puzzleNum;
    bool puzzleAreaClear;
    public GameObject[] puzzleAreaDoor;
    public GameObject puzzleAreaCamera;

    // Start is called before the first frame update
    void Start()
    {
        keyNum = 2;
        keyCollected = 0;
        isEscapeMode = false;
        exploreAreaClear = false;
        puzzleAreaClear = false;
        puzzleComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isEscapeMode)

        {
            if (keyCollected == keyNum && !exploreAreaClear)
            {
                exploreAreaClear = true;
                OpenDoor(exploreAreaDoor, exploreAreaCamera);
            }

            if (puzzleComplete)
            {
                characterCamera.SetActive(true);
                puzzleComplete = false;

                if (puzzleSloved == puzzleNum && !puzzleAreaClear)
                {
                    puzzleAreaClear = true;
                    OpenDoor(puzzleAreaDoor, puzzleAreaCamera);
                }
            }
        }

        
    }

    void OpenDoor(GameObject[] door, GameObject camera)
    {
        characterCamera.SetActive(false);
        camera.SetActive(true);
        StartCoroutine(StopCutScene(camera));

        for (int i = 0; i < door.Length; i++)
        {
            door[i].GetComponent<DoorControl>().doorOpening = true;
        }
    }

    IEnumerator StopCutScene(GameObject camera)
    {
        yield return new WaitForSeconds(cutSceneDuration);
        camera.SetActive(false);
        characterCamera.SetActive(true); 
    }
}
