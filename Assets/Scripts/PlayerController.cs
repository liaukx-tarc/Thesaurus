using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float rotationX, rotationY;
    public float speed;
    public Animator armAnim;
    public Animator modelAnim;
    static public bool isAttack;
    private Transform fpCamera;
    public Transform handCamera;
    private float handAngle;
    static public Vector3 position;
    static public bool isDead;
    public float deathAngle;
    public float stamina;
    public bool isRunning;
    public bool reachMinStamina;
    static public bool isInsideHouse;
    public GameObject blood;

    private Vector3 mInput;
    public CharacterController controller;

    public RaycastHit hit;

    static public int currentHp;
    public int maxHp;
    static public float regenTimer;

    //Check House
    public Transform[] housePosition;
    public int houseTriggerDistance;
    public bool[] isNearHouse;
    public GameObject[] houseInterior;

    private bool startRun;
    public GameObject magicLight;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        maxHp = 2;
        currentHp = maxHp;
        regenTimer = 5.0f;
        rotationX = 0;
        rotationY = 0;
        fpCamera = this.gameObject.transform.GetChild(2).transform;
        controller = GetComponent<CharacterController>();
        isAttack = false;
        isDead = false;
        deathAngle = 0;
        position = this.transform.position;
        handAngle = handCamera.eulerAngles.x;
        stamina = 20;
        isRunning = false;
        reachMinStamina = true;
        isInsideHouse = false;
        startRun = false;
        rotationX = transform.eulerAngles.y;

        //Check House
        isNearHouse = new bool[housePosition.Length];

        for (int i = 0; i < isNearHouse.Length; i++)
        {
            isNearHouse[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(WorldController.startEndScene)
        {
            if(!startRun)
            {
                transform.position = new Vector3(9.60f, 10.0f, 38.93f);
                transform.eulerAngles = new Vector3(0, 0, 0);
                startRun = true;
                modelAnim.SetBool("isRunning", true);
                magicLight.GetComponent<Light>().intensity = 0;
            }
            else
            {
                transform.Translate(transform.forward * Time.deltaTime *10);
            }
        }
        else
        {
            if (currentHp > 0)
            {
                if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {
                    armAnim.SetBool("isWalking", true);
                }
                else
                {
                    armAnim.SetBool("isWalking", false);
                }
                mInput = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
                transform.TransformDirection(mInput);
                controller.SimpleMove(mInput * speed);
                if (Input.GetButton("Run"))
                {
                    if (stamina > 0 && reachMinStamina)
                    {
                        armAnim.SetBool("isRunning", true);
                        speed = 8;
                        isRunning = true;
                    }
                    else
                    {
                        armAnim.SetBool("isRunning", false);
                        speed = 5;
                        isRunning = false;
                    }
                }
                else
                {
                    armAnim.SetBool("isRunning", false);
                    speed = 5;
                    isRunning = false;
                }
                if (isAttack)
                {
                    armAnim.SetTrigger("isAttack");
                    isAttack = false;
                }
                if (isRunning)
                {
                    stamina -= Time.deltaTime * 1.25f;
                }
                else if (stamina < 5 && !isRunning)
                {
                    stamina += Time.deltaTime * 2.0f;
                }
                if (stamina <= 0)
                {
                    reachMinStamina = false;
                }
                if (!reachMinStamina)
                {
                    if (stamina > 3)
                        reachMinStamina = true;
                }
                if(Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
                {
                    rotationX += Input.GetAxisRaw("Mouse X") * Time.deltaTime * 100;
                    rotationY -= Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 100;
                    transform.eulerAngles = new Vector3(0.0f, rotationX, 0.0f);
                    rotationY = Mathf.Clamp(rotationY, -45f, 45f);
                    fpCamera.eulerAngles = new Vector3(rotationY, fpCamera.eulerAngles.y, fpCamera.eulerAngles.z);
                }
                if (currentHp < maxHp)
                {
                    if (regenTimer > 0)
                    {
                        regenTimer -= Time.deltaTime;
                        blood.GetComponent<UnityEngine.UI.RawImage>().color = new Color(155 / 255.0f, 0, 0, Mathf.Lerp(0, 1.0f,(regenTimer / 5)));
                    }
                    else
                    {
                        currentHp++;
                        regenTimer = 5.0f;
                    }
                }
            }
            else
            {
                Debug.Log("dead");
                blood.GetComponent<UnityEngine.UI.RawImage>().color = new Color(155 / 255.0f, 0, 0, 1.0f);
                isDead = true;
                if (deathAngle < 80)
                {
                    deathAngle += 1;
                }
                if (handAngle > 0)
                {
                    handAngle -= 1;
                }
                transform.eulerAngles = new Vector3(deathAngle, rotationX, 0.0f);
                fpCamera.eulerAngles = new Vector3(rotationY, fpCamera.eulerAngles.y, fpCamera.eulerAngles.z);
                handCamera.eulerAngles = new Vector3(handAngle, handCamera.eulerAngles.y, handCamera.eulerAngles.z);
            }
        }

        position = this.transform.position;

        CheckHouseDistance();
    }

    void CheckHouseDistance()
    {
        for (int i = 0; i < housePosition.Length; i++)
        {
            if (Vector3.Distance(housePosition[i].position, this.transform.position) < houseTriggerDistance)
            {
                if (!isNearHouse[i])
                {
                    houseInterior[i].SetActive(true);
                    isNearHouse[i] = true;
                }
            }

            else
            {
                if (isNearHouse[i])
                {
                    houseInterior[i].SetActive(false);
                    isNearHouse[i] = false;
                }
            }
        }
    }
}
