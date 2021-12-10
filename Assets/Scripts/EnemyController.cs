using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Animator anim;
    private float atkCD;
    private float atkDelayTime;
    private NavMeshAgent agent;

    public bool isStun;
    public bool isStunStart;
    public bool isStunFinish;
    public float stunTime;

    public bool isSlow;
    public bool isSlowStart;
    public float moveSpeed;

    public int magicType;
    public Color[] hitColor;

    public Transform[] doorPosition;
    public bool isNearDoor;

    public bool isLastSpawn;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        atkCD = 0;
        stunTime = 0;
        isStun = false;
        isStunStart = false;
        isStunFinish = false;
        isSlow = false;
        isSlowStart = false;
        moveSpeed = 1.0f;
        magicType = 0;
        atkDelayTime = 0.5f;
        isNearDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (atkCD > 0)
        {
            atkCD -= Time.deltaTime;
        }
        if (isStun)
        {
            anim.speed = 0;
            agent.speed = 0.0f;
            GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("Vector1_99b657cddeea437c997f08ebee7e2c31", 1f);
            GetComponentInChildren<SkinnedMeshRenderer>().material.SetColor("Color_20489dc9a40f4e2d9c702621e1fa832d", hitColor[magicType]);
            
            if (!isStunStart)
            {
                StartCoroutine(Stun());
                isStunStart = true;
            }
        }
        else
        {
            if (isSlow)
            {
                anim.speed = 0.5f;
                moveSpeed = 0.5f;
                if(!isSlowStart)
                {
                    StartCoroutine(Slow());
                    isSlowStart = true;
                }
                GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("Vector1_99b657cddeea437c997f08ebee7e2c31", 1f);
                GetComponentInChildren<SkinnedMeshRenderer>().material.SetColor("Color_20489dc9a40f4e2d9c702621e1fa832d", hitColor[magicType]);
            }

            if (GetComponentInChildren<DetectPlayer>().isIdle)
            {
                agent.speed = 0.0f;
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", false);
            }
            else
            {
                if (GetComponentInChildren<DetectPlayer>().isPlayerDetected)
                {
                    if (GetComponentInChildren<DetectPlayer>().isNear)
                    {
                        agent.speed = 0.0f;
                        anim.SetBool("isRunning", false);
                        anim.SetBool("isWalking", false);
                        if (atkCD <= 0)
                        {
                            anim.SetTrigger("isAttack");
                            atkCD = 2;
                            StartCoroutine(AtkDelay());
                        }
                    }
                    else
                    {
                        for (int i = 0; i < doorPosition.Length; i++)
                        {
                            if (Vector3.Distance(doorPosition[i].position, this.transform.position) < 2.0f)
                            {
                                isNearDoor = true;
                                break;
                            }
                        }
                        if (isNearDoor && PlayerController.isInsideHouse)
                        {
                            agent.speed = 0.0f;
                            anim.SetBool("isRunning", false);
                            anim.SetBool("isWalking", false);
                        }
                        else
                        {
                            agent.speed = 8.0f * moveSpeed;
                            anim.SetBool("isRunning", true);
                            anim.SetBool("isWalking", false);
                        }
                    }
                }
                else
                {
                    isNearDoor = false;
                    agent.speed = 2f * moveSpeed;
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isWalking", true);
                }
            }
        }
    }

    IEnumerator Stun()
    {
        if(magicType == 0)
        {
            stunTime = 1.5f;
        }
        else if (magicType == 1)
        {
            stunTime = 3.0f;
        }
        yield return new WaitForSeconds(stunTime);
        anim.speed = 1;
        GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("Vector1_99b657cddeea437c997f08ebee7e2c31", 0);
        isStun = false;
        isStunStart = false;
        isStunFinish = true;
    }
    IEnumerator Slow()
    {
        yield return new WaitForSeconds(4.0f);
        GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("Vector1_99b657cddeea437c997f08ebee7e2c31", 0);
        isSlow = false;
        isSlowStart = false;
        anim.speed = 1.0f;
        moveSpeed = 1.0f;
    }
    IEnumerator AtkDelay()
    {
        if(isSlow)
        {
            atkDelayTime = 1.0f;
        }
        else
        {
            atkDelayTime = 0.5f;
        }
        yield return new WaitForSeconds(atkDelayTime);
        if(!isStun && agent.remainingDistance < 2f)
        {
            PlayerController.currentHp--;
            PlayerController.regenTimer = 5.0f;
        }
    }
}
