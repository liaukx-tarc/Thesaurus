using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    public int buttonNum;
    public bool testBool;

    string spellTag;
    string colorName;
    GhostLeg ghostLeg;
    GameObject light;
    // Start is called before the first frame update
    void Start()
    {
        spellTag = "Spell";
        colorName = "_Color";
        ghostLeg = gameObject.GetComponentInParent<GhostLeg>();
        light = this.transform.GetChild(1).gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(spellTag))
        {
            if (buttonNum == ghostLeg.ansNum)
            {
                ghostLeg.isActivate = true;
                light.GetComponent<Renderer>().material.color = new Color(0.45f, 1.0f, 0.15f);
            }
            else
                light.GetComponent<Renderer>().material.color = new Color(0.79f, 0.06f, 0.19f);
        }
    }

    private void Update()
    {
        if (testBool)
        {
            if (buttonNum == ghostLeg.ansNum)
            {
                ghostLeg.isActivate = true;
                light.GetComponent<Renderer>().material.color = new Color(0.45f, 1.0f, 0.15f);
            }     
            else
                light.GetComponent<Renderer>().material.color = new Color(0.79f, 0.06f, 0.19f);
        }
    }
}
