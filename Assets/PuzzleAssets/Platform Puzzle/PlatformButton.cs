using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    public int buttonNum;
    public float waitingTime;
    public bool isHit;
    public GameObject ansPrefab;

    bool isActive;
    string spellTag;
    string colorName;
    GhostLeg ghostLeg;
    GameObject light;

    public AudioSource audioSource;
    public AudioSource[] platformAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        colorName = "_Color";
        ghostLeg = gameObject.GetComponentInParent<GhostLeg>();
        light = this.transform.GetChild(1).gameObject;
        isHit = false;
    }


    private void Update()
    {
        if (isHit && !isActive)
        {
            audioSource.Play();

            StartCoroutine(ShowAns(waitingTime));

            isHit = false;
            isActive = true;

            GameObject AnsObj = Instantiate(ansPrefab, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f,
                   this.transform.position.z + 0.2f), Quaternion.identity, gameObject.transform);

            AnsObj.GetComponent<GhostLegAns>().inti(buttonNum,
                GetComponentInParent<GhostLeg>().row,
                GetComponentInParent<GhostLeg>().col,
                GetComponentInParent<GhostLeg>().footArr);
        }
    }

    IEnumerator ShowAns(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime / 9 * GetComponentInParent<GhostLeg>().row);
        if (buttonNum == ghostLeg.ansNum)
        {
            for (int i = 0; i < platformAudioSource.Length; i++)
            {
                platformAudioSource[i].Play();
            }

            ghostLeg.isActivate = true;
            light.GetComponent<Renderer>().material.color = new Color(0.45f, 1.0f, 0.15f);
            WorldController.platformPuzzleNum++;
        }
        else
        {
            light.GetComponent<Renderer>().material.color = new Color(0.79f, 0.06f, 0.19f);
            PlayerController.currentHp--;
        }
            
            
    }
}
