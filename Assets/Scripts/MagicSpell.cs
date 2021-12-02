using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpell : MonoBehaviour
{
    public GameObject projectile;
    private float atkTimer;
    // Start is called before the first frame update
    void Start()
    {
        atkTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (atkTimer <= 0.0f && Input.GetButtonDown("Fire1"))
        {
            PlayerController.isAttack = true;
            Instantiate(projectile, transform.position, transform.rotation);
            atkTimer = 3.0f;
        }
        else if (atkTimer > 0.0f)
        {
            atkTimer -= Time.deltaTime;
        }
    }
}
