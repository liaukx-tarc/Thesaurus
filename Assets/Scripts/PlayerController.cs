using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float rotationX, rotationY;
    public float speed;
    public Animator armAnim;
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

    private Vector3 mInput;
    public CharacterController controller;

    public RaycastHit hit;

    static public int currentHp;
    public int maxHp;
    static public float regenTimer;
    private int layerMask = 1 << 9;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = 2;
        currentHp = maxHp;
        regenTimer = 0;
        rotationX = 0;
        rotationY = 0;
        fpCamera = this.gameObject.transform.GetChild(2).transform;
        controller = GetComponent<CharacterController>();
        isAttack = false;
        isDead = false;
        deathAngle = 0;
        position = this.transform.position;
        handAngle = handCamera.eulerAngles.x;
        stamina = 5;
        isRunning = false;
        reachMinStamina = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(stamina);
        if(currentHp > 0)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                armAnim.SetBool("isWalking", true);
            }
            else
            {
                armAnim.SetBool("isWalking", false);
            }
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

            if(Physics.Raycast(fpCamera.position, transform.forward, out hit, 3, layerMask))
            {
                Debug.Log(fpCamera.position);
                Debug.Log(hit.collider.gameObject.name);
                if(hit.collider.gameObject.tag == "Door")
                {

                }
                else if (hit.collider.gameObject.tag == "Key")
                {

                }
                else //puzzle
                {

                }
;            }

            if (isAttack)
            {
                armAnim.SetTrigger("isAttack");
                isAttack = false;
            }
            if(isRunning)
            {
                stamina -= Time.deltaTime *1.5f;
            }
            else if(stamina < 5 && !isRunning)
            {
                stamina += Time.deltaTime;
            }
            if(stamina <= 0)
            {
                reachMinStamina = false;
            }
            if(!reachMinStamina)
            {
                if (stamina > 2)
                    reachMinStamina = true;
            }
            rotationX += Input.GetAxisRaw("Mouse X") * Time.deltaTime * 200;
            rotationY -= Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 200;
            mInput = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
            transform.TransformDirection(mInput);
            controller.SimpleMove(mInput * speed);
            transform.eulerAngles = new Vector3(0.0f, rotationX, 0.0f);
            rotationY = Mathf.Clamp(rotationY, -30f, 30f);
            fpCamera.eulerAngles = new Vector3(rotationY, fpCamera.eulerAngles.y, fpCamera.eulerAngles.z);
            if(currentHp < maxHp)
            {
                if (regenTimer > 0)
                {
                    regenTimer -= Time.deltaTime;
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
            isDead = true;
            if(deathAngle < 90)
            {
                deathAngle += 1;
            }
            if(handAngle > 0)
            {
                handAngle -= 1;
            }
            transform.eulerAngles = new Vector3(deathAngle, rotationX, 0.0f);
            fpCamera.eulerAngles = new Vector3(rotationY, fpCamera.eulerAngles.y, fpCamera.eulerAngles.z);
            handCamera.eulerAngles = new Vector3(handAngle, handCamera.eulerAngles.y, handCamera.eulerAngles.z);
        }
        position = this.transform.position;
    }
    //private void LateUpdate()
    //{
    //    this.gameObject.transform.Rotate(rotationY, 0, 0);
    //}
}
