using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    private Animator anim;
    private float atkCD;
    private NavMeshAgent agent;
    public bool isHit;
    public bool isStun;
    public bool stunFinish;
    public bool isAtk;
    public float stunTimer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        atkCD = 0;
        stunTimer = 0;
        isHit = false;
        isStun = false;
        stunFinish = false;
        isAtk = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = PlayerController.position;
        if (!isAtk)
        {
            agent.speed = 4.0f;
            anim.SetBool("isRunning", true);
        }
        Debug.Log(agent.remainingDistance);
        if (atkCD > 0)
        {
            atkCD -= Time.deltaTime;
        }
        if (agent.remainingDistance < 3f && agent.remainingDistance > 0.1f)
        {
            agent.speed = 0.0f;
            anim.SetBool("isRunning", false);
            if (atkCD <= 0)
            {
                anim.SetTrigger("isAttack");
                atkCD = 3;
                StartCoroutine(AtkDelay());
                isAtk = true;
                StartCoroutine(AtkIdle());
            }
        }
    }
    IEnumerator AtkDelay()
    {
        yield return new WaitForSeconds(1f);
        if(agent.remainingDistance < 3f)
        {
            PlayerController.currentHp = 0;
        }
    }
    IEnumerator AtkIdle()
    {
        yield return new WaitForSeconds(2f);
        isAtk = false;
    }
    //    if(isHit)
    //    {
    //        anim.speed = 0;
    //        agent.speed = 0.0f;
    //        GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("Vector1_99b657cddeea437c997f08ebee7e2c31", 1f);
    //        if(!isStun)
    //        {
    //            StartCoroutine(Stun());
    //            isStun = true;
    //        }
    //    }
    //    else
    //    {
    //        if (atkCD > 0)
    //        {
    //            atkCD -= Time.deltaTime;
    //        }
    //        if (GetComponentInChildren<DetectPlayer>().isIdle)
    //        {
    //            agent.speed = 0.0f;
    //            anim.SetBool("isRunning", false);
    //            anim.SetBool("isWalking", false);
    //        }
    //        else
    //        {
    //            if (GetComponentInChildren<DetectPlayer>().isPlayerDetected)
    //            {
    //                if (GetComponentInChildren<DetectPlayer>().isNear)
    //                {
    //                    agent.speed = 0.0f;
    //                    anim.SetBool("isRunning", false);
    //                    anim.SetBool("isWalking", false);
    //                    if (atkCD <= 0)
    //                    {
    //                        anim.SetTrigger("isAttack");
    //                        atkCD = 2;
    //                        PlayerController.currentHp--;
    //                        PlayerController.regenTimer = 5.0f;
    //                    }
    //                }
    //                else
    //                {
    //                    agent.speed = 4.0f;
    //                    anim.SetBool("isRunning", true);
    //                }
    //            }
    //            else
    //            {
    //                agent.speed = 1.5f;
    //                anim.SetBool("isRunning", false);
    //                anim.SetBool("isWalking", true);
    //            }
    //        }
    //    }
    //}

    //IEnumerator Stun()
    //{
    //    yield return new WaitForSeconds(2.0f);
    //    anim.speed = 1;
    //    GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("Vector1_99b657cddeea437c997f08ebee7e2c31", 0);
    //    isHit = false;
    //    isStun = false;
    //    stunFinish = true;
    //}
}
