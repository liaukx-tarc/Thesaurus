using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    public float cutSceneDuration;
    public GameObject characterCamera;
    static public bool isEscapeMode;

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

    //Mana Ball
    static public int manaBallNum;
    public int manaBallNeeded;
    public GameObject manaBallCollection;
    public GameObject cityBorder;
    bool borderClose;

    //dead scene
    public GameObject deadScene;
    private float deadAlpha;

    // Start is called before the first frame update
    void Start()
    {
        keyNum = 2;
        keyCollected = 0;
        isEscapeMode = false;
        exploreAreaClear = false;
        puzzleAreaClear = false;
        puzzleComplete = false;
        borderClose = false;

        deadAlpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerController.isDead)
        {
            if (!isEscapeMode)

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

            else
            {
                if (manaBallNum >= manaBallNeeded && !borderClose)
                {
                    StartCoroutine(BorderDisable());
                }
            }
        }
       else
        {
            if(deadAlpha < 1)
            {
                deadAlpha += Time.deltaTime * 0.5f;
            }
            deadScene.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, deadAlpha);
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

    public void IntiEscapePhase()
    {
        manaBallCollection.SetActive(true);
        cityBorder.SetActive(true);
        manaBallNum = 0;
    }
    IEnumerator BorderDisable()
    {
        yield return new WaitForSeconds(1f);
        
        cityBorder.GetComponent<Renderer>().material.SetFloat("Vector1_83a9b54fc6b549fe90acb3e18d8bec2a",
            cityBorder.GetComponent<Renderer>().material.GetFloat("Vector1_83a9b54fc6b549fe90acb3e18d8bec2a") - 0.005f);
        
        if (cityBorder.GetComponent<Renderer>().material.GetFloat("Vector1_83a9b54fc6b549fe90acb3e18d8bec2a") <= 0)
        {
            borderClose = true;
            cityBorder.SetActive(false);
        }
    }
}
