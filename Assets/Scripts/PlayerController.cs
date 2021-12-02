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

    private Vector3 mInput;
    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        rotationX = 0;
        rotationY = 0;
        fpCamera = this.gameObject.transform.GetChild(2).transform;
        controller = GetComponent<CharacterController>();
        isAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            armAnim.SetBool("isWalking", true);
        }
        else
        {
            armAnim.SetBool("isWalking", false);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            armAnim.SetBool("isRunning", true);
            speed = 6;
        }
        else
        {
            armAnim.SetBool("isRunning", false);
            speed = 4;
        }
        if(isAttack)
        {
            armAnim.SetTrigger("isAttack");
            isAttack = false;
        }
        rotationX += Input.GetAxisRaw("Mouse X") * Time.deltaTime * 200;
        rotationY -= Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 200;
        mInput = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
        transform.TransformDirection(mInput);
        controller.SimpleMove(mInput *speed);
        transform.eulerAngles = new Vector3(0.0f, rotationX, 0.0f);
        rotationY = Mathf.Clamp(rotationY, -30f, 30f);
        fpCamera.eulerAngles = new Vector3(rotationY, fpCamera.eulerAngles.y, fpCamera.eulerAngles.z);
    }
    //private void LateUpdate()
    //{
    //    this.gameObject.transform.Rotate(rotationY, 0, 0);
    //}
}
