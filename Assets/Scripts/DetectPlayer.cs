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
        agent.destination = patrolPoint[index].position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position + transform.up, transform.forward * 12.0f, Color.red);
        Debug.Log(isPlayerDetected);
        if(!controller.isStun)
        {
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
                    if (!agent.pathPending && agent.remainingDistance < 1.5f)
                    {
                        index++;
                        index = index % patrolPoint.Length;
                        agent.destination = patrolPoint[index].position;
                        StartCoroutine(Idle());
                    }
                }
            }
            if(controller.isStunFinish)
            {
                chaseTimer = 3;
                ChaseTarget();
                controller.isStunFinish = false;
            }
            if(controller.isSlow)
            {
                chaseTimer = 3;
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
                            chaseTimer = 3;
                            if (Vector3.Distance(transform.position, target.position) < 2.5f)
                            {
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
                else if (Vector3.Distance(transform.position, target.position) < 6f)
                {
                    isNear = false;
                    chaseTimer = 3;
                    ChaseTarget();
                }
            }
        }
    }

    void ChaseTarget()
    {
        isPlayerDetected = true;
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
