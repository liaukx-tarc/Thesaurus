using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpell : MonoBehaviour
{
    public GameObject projectile;
    private float atkTimer;
    public RaycastHit hit;
    public bool isAttackDelay;
    private float attackDelay;
    int layerMask = 1 << 8;
    // Start is called before the first frame update
    void Start()
    {
        atkTimer = 0.0f;
        isAttackDelay = false;
        attackDelay = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAttackDelay)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100, layerMask)
                || Physics.Raycast(transform.position - new Vector3(0.2f, 0, 0), transform.forward, out hit, 100, layerMask)
                || Physics.Raycast(transform.position + new Vector3(0.2f, 0, 0), transform.forward, out hit, 100, layerMask)
                || Physics.Raycast(transform.position - new Vector3(0, 0.2f, 0), transform.forward, out hit, 100, layerMask)
                || Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), transform.forward, out hit, 100, layerMask))
            {
                if (hit.collider.tag == "Enemy")
                {
                    Debug.Log(hit.collider.name);
                    hit.collider.GetComponentInParent<EnemyController>().isHit = true;
                }
            }
            attackDelay -= Time.deltaTime;
        }
        if(attackDelay <= 0)
        {
            isAttackDelay = false;
        }
        Debug.DrawRay(transform.position, transform.forward * 1000.0f, Color.red);
        if (atkTimer <= 0.0f && Input.GetButtonDown("Fire1"))
        {
            PlayerController.isAttack = true;
            Instantiate(projectile, transform.position, transform.rotation);
            isAttackDelay = true;
            attackDelay = 0.1f;
            atkTimer = 3.0f;
        }
        else if (atkTimer > 0.0f)
        {
            atkTimer -= Time.deltaTime;
        }
    }
}
