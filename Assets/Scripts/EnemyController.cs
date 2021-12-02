using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    private CharacterController character;
    private Animator anim;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }  
        if (DetectPlayer.isIdle)
        {
            GetComponent<AIPath>().maxSpeed = 0.0f;
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
        }
        else
        {
            if (DetectPlayer.isPlayerDetected)
            {
                if (DetectPlayer.isNear)
                {
                    GetComponent<AIPath>().maxSpeed = 0.0f;
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isWalking", false);
                    if (timer <= 0)
                    {
                        anim.SetTrigger("isAttack");
                        timer = 2;
                    }
                }
                else
                {
                    GetComponent<AIPath>().maxSpeed = 2.0f;
                    anim.SetBool("isRunning", true);
                }
            }
            else
            {
                GetComponent<AIPath>().maxSpeed = 1.0f;
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", true);
            }
        }
        
    }
}
