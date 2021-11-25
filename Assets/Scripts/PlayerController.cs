using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float rotationX, rotationY, speed;
    public Animator animator;
    public Animator animator2;
    private Transform fpCamera;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
        rotationX = 0;
        rotationY = 0;
        fpCamera = this.gameObject.transform.GetChild(2).transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            animator.SetBool("isWalking", true);
            animator2.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator2.SetBool("isWalking", false);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", true);
            animator2.SetBool("isRunning", true);
            speed = 3;
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator2.SetBool("isRunning", false);
            speed = 2;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("isAttack");
            animator2.SetTrigger("isAttack");
            Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z), transform.rotation);
        }
        rotationX += Input.GetAxisRaw("Mouse X") * Time.deltaTime * 200;
        rotationY -= Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 200;
        transform.Translate(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed);
        transform.eulerAngles = new Vector3(0, rotationX, 0.0f);
        rotationY = Mathf.Clamp(rotationY, -30f, 30f);
        fpCamera.eulerAngles = new Vector3(rotationY, rotationX, 0.0f);
    }
    //private void LateUpdate()
    //{
    //    this.gameObject.transform.Rotate(rotationY, 0, 0);
    //}
}
