using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public static int platformPuzzleNum;
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

    //mana bar
    public GameObject manaBar;

    //quest
    public GameObject quest;
    public GameObject questContext;
    public bool showQuest;

    //dead scene
    public GameObject blackScene;
    private float blackAlpha;
    public GameObject deathMenu;

    //end scene
    public GameObject[] entranceDoor;
    static public bool startEndScene;
    public GameObject endCamera;
    public bool isSceneComplete;

    //Win Menu
    public GameObject winMenu;
    public GameObject winText;
    public GameObject congratulationsText;
    public GameObject winButton;
    bool isWinText;
    float textAlpha;

    //Pause Menu
    public GameObject pauseMenu;
    public GameObject instruction;
    public GameObject pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        keyNum = 2;
        keyCollected = 0;

        platformPuzzleNum = 0;

        puzzleSloved = 0;
        isEscapeMode = true;

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
        isInPuzzle = false;
        puzzleUnlock = false;
        isWinText = false;
        showQuest = false;

        CheckEndGame.manaballNeeded = manaBallNeeded;
        ManaBallCollect.manaBallNeeded = manaBallNeeded;

        blackAlpha = 0;
        textAlpha = 0;
        manaBallNum = 0;

        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(QuestAppear());

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.isDead)
        {
            Quest();
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
                    isInPuzzle = false;

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
                        AudioManager.playManaBallSpawnSound = true;
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

                if (isWin)
                {
                    if (!startEndScene)
                    {
                        uiCanvas.SetActive(false);
                        characterCamera.GetComponentInChildren<Camera>().enabled = false;
                        endCamera.SetActive(true);
                        startEndScene = true;
                        StartCoroutine(StartEndScreen());
                    }

                    if (isSceneComplete)
                    {
                        blackScene.SetActive(true);
                        if (blackAlpha < 1)
                        {
                            blackAlpha += Time.deltaTime * 0.5f;
                        }

                        else
                        {
                            if (!isWinText)
                            {
                                if (textAlpha < 1)
                                    textAlpha += Time.deltaTime * 0.5f;
                                else
                                {
                                    congratulationsText.SetActive(false);
                                    winText.SetActive(true);
                                    isWinText = true;
                                    textAlpha = 0;
                                }
                                congratulationsText.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, textAlpha);
                            }

                            else
                            {
                                if (textAlpha < 1)
                                    textAlpha += Time.deltaTime * 0.5f;
                                else
                                    winButton.SetActive(true);
                                winText.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, textAlpha);
                            }
                        }
                        blackScene.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, blackAlpha);
                        winMenu.SetActive(true);
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                }
            }

            if (!isWin)
            {
                if(showQuest)
                {
                    if (Input.GetButton("Quest"))
                    {
                        quest.SetActive(true);
                    }
                    else
                    {
                        quest.SetActive(false);
                    }
                }
                if (Input.GetButtonDown("Escape"))
                {
                    if (Time.timeScale != 0)
                    {
                        if (!isInPuzzle)
                        {
                            blackScene.SetActive(true);
                            blackAlpha = 0.75f;
                            blackScene.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, blackAlpha);

                            instruction.SetActive(false);
                            pauseButton.SetActive(true);
                            uiCanvas.SetActive(false);
                            pauseMenu.SetActive(true);
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                            Time.timeScale = 0;
                        }

                        else
                        {
                            if (puzzleUnlock)
                            {
                                isInPuzzle = false;
                                characterCamera.SetActive(true);
                                uiCanvas.SetActive(true);
                            }
                        }
                    }

                    else
                    {
                        blackAlpha = 0;
                        blackScene.SetActive(false);
                        blackScene.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, blackAlpha);

                        pauseMenu.SetActive(false);
                        uiCanvas.SetActive(true);
                        Time.timeScale = 1;
                        Cursor.lockState = CursorLockMode.Locked;
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

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            deathMenu.SetActive(true);
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
        AudioManager.playBorderSound = true;
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

    public void Quest()
    {
        questContext.GetComponent<TextMeshProUGUI>().color = new Color(180 / 255.0f, 180 / 255.0f, 180 / 255.0f, 1);
        if (!exploreAreaClear)
        {
            questContext.GetComponent<TextMeshProUGUI>().text = "Collect key parts to unlock the main gate (" + keyCollected + "/"+ keyNum +")";
        }
        else if (platformPuzzleNum < 3)
        {
            questContext.GetComponent<TextMeshProUGUI>().text = "Solve platform puzzles (" + platformPuzzleNum + "/" + 3 + ")";
        }
        else if (puzzleSloved < 3)
        {
            questContext.GetComponent<TextMeshProUGUI>().text = "Investigate crystals in the garden (" + puzzleSloved + "/" + 3 + ")";
        }
        else if (!isEscapeMode)
        {
            questContext.GetComponent<TextMeshProUGUI>().text = "Collect Spell Book in the temple";
        }
        else if(manaBallNum < manaBallNeeded)
        {
            questContext.GetComponent<TextMeshProUGUI>().text = "Collect energy ball to break the magic border (" + manaBallNum + "/" + manaBallNeeded + ")";
        }
        else
        {
            questContext.GetComponent<TextMeshProUGUI>().text = "Escape from here !";
        }
    }

    IEnumerator SpawnNormal()
    {
        yield return new WaitForSeconds(6.0f);
        characterCamera.SetActive(true);
        bossCamera.SetActive(false);
        spawnMonster.SetActive(true);
        manaBar.SetActive(true);
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
    IEnumerator QuestAppear()
    {
        yield return new WaitForSeconds(3.0f);
        quest.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        quest.SetActive(false);
        showQuest = true;
    }
}
