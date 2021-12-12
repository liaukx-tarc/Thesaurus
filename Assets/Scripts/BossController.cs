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
    static public bool roarComplete;
    private bool isRoar;
    private bool isStart;
    private Vector3 endPosition;
    private bool endRoar;

    public AudioSource bossSound;
    public AudioSource attackSound;
    public AudioClip roar;
    public AudioClip attack;
    public AudioClip run;
    bool isPlayRunSound;

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
        isRoar = false;
        isStart = false;
        endPosition = new Vector3(9.60f, 10.0f, 35f);
        endRoar = false;
        isPlayRunSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!roarComplete)
        {
            anim.SetBool("isRunning", false);
            if (!isRoar)
            {
                bossSound.clip = roar;
                bossSound.Play();
                agent.speed = 0.0f;
                anim.SetTrigger("isRoar");
                StartCoroutine(RoarEnd());
            }
            isRoar = true;
        }
        else
        {
            if (WorldController.startEndScene)
            {
                if (!isStart)
                {
                    agent.ResetPath();
                    agent.enabled = false;
                    transform.position = endPosition;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    isStart = true;
                    anim.SetBool("isRunning", true);
                }
                else
                {
                    if (Vector3.Distance(transform.position, endPosition) < 12.0f)
                    {
                        transform.Translate(transform.forward * Time.deltaTime * 10);
                    }
                    else
                    {
                        if(!endRoar)
                        {
                            roarComplete = false;
                            endRoar = true;
                        }
                    }
                }
            }
            else
            {
                agent.destination = PlayerController.position;
                if (!isAtk)
                {
                    if(!isPlayRunSound)
                    {
                        bossSound.Stop();
                        bossSound.clip = run;
                        bossSound.Play();
                        isPlayRunSound = true;
                    }
                    
                    if (agent.remainingDistance > 10f)
                    {
                        agent.speed = 15.0f;

                    }
                    else
                    {
                        agent.speed = 6.0f;
                    }
                    anim.SetBool("isRunning", true);
                }
                //Debug.Log(float.IsInfinity(agent.remainingDistance));
                if (atkCD > 0)
                {
                    atkCD -= Time.deltaTime;
                }
                if (agent.remainingDistance < 4f && agent.remainingDistance > 0.1f)
                {
                    bossSound.Stop();
                    isPlayRunSound = false;
                    agent.speed = 0;
                    anim.SetBool("isRunning", false);
                    if (atkCD <= 0)
                    {
                        if (!PlayerController.isDead)
                        {
                            attackSound.clip = attack;
                            attackSound.Play();
                            anim.SetTrigger("isAttack");
                            atkCD = 3;
                            StartCoroutine(AtkDelay());
                            isAtk = true;
                            StartCoroutine(AtkIdle());
                        }
                    }
                }
            }  
        }
    }

    IEnumerator AtkDelay()
    {
        yield return new WaitForSeconds(1f);
        if (agent.remainingDistance < 5f)
        {
            AudioManager.playDeath = true;
            PlayerController.currentHp = 0;
        }
    }
    IEnumerator AtkIdle()
    {
        yield return new WaitForSeconds(2f);
        isAtk = false;
    }
    IEnumerator RoarEnd()
    {
        yield return new WaitForSeconds(7f);
        roarComplete = true;
        isRoar = false;
    }
}
