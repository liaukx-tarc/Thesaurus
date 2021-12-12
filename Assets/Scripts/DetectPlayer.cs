using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectPlayer : MonoBehaviour
{
    public bool isPlayerDetected;
    public bool isIdle;
    public bool isNear;
    private EnemyController controller;
    private RaycastHit hit;
    private Vector3 direction;
    private Transform target;
    public Transform[] patrolPoint;
    private NavMeshAgent agent;
    public float delay;
    private float angle;
    private float chaseTimer;
    public float chaseTime;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        isPlayerDetected = false;
        isIdle = false;
        isNear = false;
        chaseTimer = 0;
        index = 0;
        agent = GetComponentInParent<NavMeshAgent>();
        controller = GetComponentInParent<EnemyController>();
        if(patrolPoint.Length != 0)
        {
            agent.destination = patrolPoint[index].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isPlayerDetected);
        if(isPlayerDetected)
        {
            agent.destination = PlayerController.position;
        }
        else
        {
            if (patrolPoint.Length != 0)
            {
                agent.destination = patrolPoint[index].position;
            }
        }
        if(!controller.isStun)
        {
            if (controller.isLastSpawn)
            {
                chaseTime = 3600;
                chaseTimer = chaseTime;
                isPlayerDetected = true;
                ChaseTarget();
            }
            else
            {
                chaseTime = 5;
            }
            if (chaseTimer > 0)
            {
                chaseTimer -= Time.deltaTime;
            }
            else
            {
                if (isPlayerDetected)
                {
                    MissTarget();
                }
                else
                {
                    if (!agent.pathPending && agent.remainingDistance < 2.1f)
                    {
                        if(patrolPoint.Length != 0)
                        {
                            index++;
                            index = index % patrolPoint.Length;
                            agent.destination = patrolPoint[index].position;
                            StartCoroutine(Idle());
                        }
                    }
                }
            }
            if(controller.isStunFinish)
            {
                chaseTimer = chaseTime;
                isPlayerDetected = true;
                ChaseTarget();
                controller.isStunFinish = false;
            }
            if(controller.isSlow)
            {
                chaseTimer = chaseTime;
                isPlayerDetected = true;
                ChaseTarget();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(!controller.isStun)
        {
            //Debug.Log(Vector3.Distance(transform.position, PlayerController.position));
            if (other.tag == "Player")
            {
                target = other.transform;
                direction = target.position - transform.position;
                angle = Vector3.Angle(direction, transform.forward);
                if (angle < 45f)
                {
                    if (Physics.Raycast(transform.position + transform.up, direction, out hit, 15.0f))
                    {
                        if (hit.collider.gameObject.tag == "Player")
                        {
                            isPlayerDetected = true;
                            chaseTimer = chaseTime;
                            if (Vector3.Distance(transform.position, target.position) < 2f)
                            {
                                agent.isStopped = true;
                                agent.enabled = false;
                                transform.LookAt(target);
                                agent.enabled = true;
                                agent.isStopped = false;
                                isNear = true;
                            }
                            else
                            {
                                isNear = false;
                                ChaseTarget();
                            }
                        }
                    }
                }
                else if (Vector3.Distance(transform.position, target.position) < 2f)
                {
                    isNear = true;
                    isPlayerDetected = true;
                    agent.isStopped = true;
                    agent.enabled = false;
                    transform.LookAt(target);
                    agent.enabled = true;
                    agent.isStopped = false;
                }
                else if (Vector3.Distance(transform.position, target.position) > 2f && Vector3.Distance(transform.position, target.position) < 6f)
                {
                    isNear = false;
                    isPlayerDetected = true;
                    chaseTimer = chaseTime;
                    ChaseTarget();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isNear = false;
            isPlayerDetected = false;
        }
    }

    void ChaseTarget()
    {
        isIdle = false;
        if(target != null)
        {
            agent.destination = target.position;
        }
        else
        {
            agent.destination = PlayerController.position;
        }
    }

    void MissTarget()
    {
        isPlayerDetected = false;
        agent.ResetPath();
        agent.destination = this.transform.position;
        target = null;
        if (!isIdle)
        {
            StartCoroutine(Idle());
        }
    }
    IEnumerator Idle()
    {
        isIdle = true;
        yield return new WaitForSeconds(2.0f);
        if (!isPlayerDetected)
        {
            agent.destination = patrolPoint[index].position;
            isIdle = false;
        }
    }

}
