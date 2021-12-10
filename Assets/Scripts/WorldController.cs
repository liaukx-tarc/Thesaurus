using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    public float cutSceneDuration;
    public GameObject characterCamera;
    static public bool isEscapeMode;
    static public bool isPlayerOut;
    static public bool isWin;
    public GameObject boss;
    public GameObject spawnMonster;
    public bool isMonsterSpawn;
    public GameObject uiCanvas;
    public GameObject bossCamera;

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
    static public bool isInPuzzle;
    static public bool puzzleUnlock;

    //Border
    public GameObject borderCamera;
    public GameObject cityBorder;
    bool borderClose, borderOpening;

    //Mana Ball
    static public int manaBallNum;
    public int manaBallNeeded;
    public GameObject manaBallCollection;
    public GameObject manaBallCamera;

    //dead scene
    public GameObject blackScene;
    private float blackAlpha;

    //end scene
    public GameObject[] entranceDoor;
    static public bool startEndScene;
    public GameObject endCamera;
    public bool isSceneComplete;

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
        borderOpening = false;
        isPlayerOut = false;
        isMonsterSpawn = false;
        isWin = false;
        startEndScene = false;
        isSceneComplete = false;

        CheckEndGame.manaballNeeded = manaBallNeeded;
        ManaBallCollect.manaBallNeeded = manaBallNeeded;

        blackAlpha = 0;
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
                    uiCanvas.SetActive(false);
                    OpenDoor(exploreAreaDoor, exploreAreaCamera);
                }

                if (puzzleComplete)
                {
                    characterCamera.SetActive(true);
                    uiCanvas.SetActive(true);
                    puzzleComplete = false;

                    if (puzzleSloved == puzzleNum && !puzzleAreaClear)
                    {
                        uiCanvas.SetActive(false);
                        puzzleAreaClear = true;
                        OpenDoor(puzzleAreaDoor, puzzleAreaCamera);
                    }
                }
            }

            else
            {
                if (borderOpening)
                {
                    StartCoroutine(BorderEnable(true));

                    if (cityBorder.GetComponent<Renderer>().material.GetFloat("Vector1_83a9b54fc6b549fe90acb3e18d8bec2a") >= 1.650f)
                    {
                        borderClose = false;
                        borderOpening = false;

                        manaBallCollection.SetActive(true);
                        manaBallNum = 0;
                        borderCamera.SetActive(false);
                        manaBallCamera.SetActive(true);
                        StartCoroutine(StopCutScene(manaBallCamera, 5));
                    }
                }

                else if (!isMonsterSpawn && isPlayerOut)
                {
                    SpawnMonster();
                    isMonsterSpawn = true;
                }

                if (manaBallNum >= manaBallNeeded && !borderClose)
                {
                    StartCoroutine(BorderEnable(false));
                    for (int i = 0; i < entranceDoor.Length; i++)
                    {
                        entranceDoor[i].GetComponent<DoorControl>().doorOpening = true;
                    }
                    borderClose = true;
                }

                if(isWin)
                {
                    if(!startEndScene)
                    {
                        uiCanvas.SetActive(false);
                        characterCamera.GetComponentInChildren<Camera>().enabled = false;
                        endCamera.SetActive(true);
                        startEndScene = true;
                        StartCoroutine(StartEndScreen());
                    }

                    if(isSceneComplete)
                    {
                        blackScene.SetActive(true);
                        if (blackAlpha < 1)
                        {
                            blackAlpha += Time.deltaTime * 0.5f;
                        }
                        blackScene.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, blackAlpha);
                    }
                }
            }
        }
       else
        {
            uiCanvas.SetActive(false);
            blackScene.SetActive(true);
            if (blackAlpha < 1)
            {
                blackAlpha += Time.deltaTime * 0.5f;
            }
            blackScene.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, blackAlpha);
        }

        if(Input.GetButtonDown("Escape"))
        {
            if(!isInPuzzle)
            {
                uiCanvas.SetActive(false);
            }

            else
            {
                if(puzzleUnlock)
                {
                    isInPuzzle = false;
                    characterCamera.SetActive(true);
                    uiCanvas.SetActive(true);
                }
            }
        }
    }

    void OpenDoor(GameObject[] door, GameObject camera)
    {
        characterCamera.SetActive(false);
        camera.SetActive(true);
        StartCoroutine(StopCutScene(camera, cutSceneDuration));

        for (int i = 0; i < door.Length; i++)
        {
            door[i].GetComponent<DoorControl>().doorOpening = true;
        }
    }

    IEnumerator StartEndScreen()
    {
        yield return new WaitForSeconds(7.0f);
        isSceneComplete = true;
    }
    
    IEnumerator StopCutScene(GameObject camera, float cutSceneDuration)
    {
        yield return new WaitForSeconds(cutSceneDuration);
        camera.SetActive(false);
        uiCanvas.SetActive(true);
        characterCamera.SetActive(true);
    }

    public void IntiEscapePhase()
    {
        cityBorder.SetActive(true);
        //Start Border Cut Scene
        uiCanvas.SetActive(false);
        characterCamera.SetActive(false);
        borderCamera.SetActive(true);
        borderOpening = true;
    }

    public void SpawnMonster()
    {
        boss.SetActive(true);
        BossController.roarComplete = false;
        characterCamera.SetActive(false);
        bossCamera.SetActive(true);
        bossCamera.GetComponent<Animator>().SetBool("isNear", true);
        StartCoroutine(SpawnNormal());
    }

    IEnumerator SpawnNormal()
    {
        yield return new WaitForSeconds(6.0f);
        characterCamera.SetActive(true);
        bossCamera.SetActive(false);
        spawnMonster.SetActive(true);
    }

    IEnumerator BorderEnable(bool state)
    {
        yield return new WaitForSeconds(1f);
        
        if(state)
        {
            cityBorder.GetComponent<Renderer>().material.SetFloat("Vector1_83a9b54fc6b549fe90acb3e18d8bec2a", 
                cityBorder.GetComponent<Renderer>().material.GetFloat("Vector1_83a9b54fc6b549fe90acb3e18d8bec2a") + 0.01f);
        }

        else
        {
            cityBorder.GetComponent<Renderer>().material.SetFloat("Vector1_83a9b54fc6b549fe90acb3e18d8bec2a",
            cityBorder.GetComponent<Renderer>().material.GetFloat("Vector1_83a9b54fc6b549fe90acb3e18d8bec2a") - 0.005f);

            if (cityBorder.GetComponent<Renderer>().material.GetFloat("Vector1_83a9b54fc6b549fe90acb3e18d8bec2a") <= 0)
            {
                borderClose = true;
                cityBorder.SetActive(false);
            }
        }
    }
}
