using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DetectPlayer : MonoBehaviour
{
    static public bool isPlayerDetected;
    static public bool isIdle;
    static public bool isNear;
    private RaycastHit hit;
    private Vector3 direction;
    private float angle;
    private float chaseTimer;
    // Start is called before the first frame update
    void Start()
    {
        isPlayerDetected = false;
        isIdle = false;
        isNear = false;
        chaseTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (AstarPath.test)
        //{
        //    this.GetComponentInParent<CharacterController>().Move(transform.forward *Time.deltaTime);
        //}

        Debug.DrawRay(transform.position + transform.up, transform.forward * 12.0f, Color.red);
        if (chaseTimer > 0)
        {
            chaseTimer -= Time.deltaTime;
        }
        else
        {
            if(isPlayerDetected)
            {
                MissTarget();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        isNear = false;
        if (other.tag == "Player")
        {
            direction = other.transform.position - transform.position;
            Debug.Log(Vector3.Distance(transform.position, other.transform.position)); 
            angle = Vector3.Angle(direction, transform.forward);
            if(angle < 45f)
            {
                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, 12.0f))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        chaseTimer = 3;
                        if (Vector3.Distance(transform.position, other.transform.position) < 1.5)
                        {
                            isNear = true;
                        }
                        else
                        {
                            ChaseTarget();
                        }
                    }
                }
            }
            else if (Vector3.Distance(transform.position, other.transform.position) < 4)
            {
                chaseTimer = 3;
                ChaseTarget();
            }
        }
    }

    void ChaseTarget()
    {
        isPlayerDetected = true;
        isIdle = false;
        this.GetComponentInParent<Patrol>().enabled = false;
        this.GetComponentInParent<AIDestinationSetter>().enabled = true;
    }

    void MissTarget()
    {
        isPlayerDetected = false;
        this.GetComponentInParent<AIDestinationSetter>().enabled = false;
        if(!isIdle)
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
            this.GetComponentInParent<Patrol>().enabled = true;
            isIdle = false;
        }
     
    }
}
