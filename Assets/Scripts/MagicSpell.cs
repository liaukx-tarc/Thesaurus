using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpell : MonoBehaviour
{
    public GameObject[] projectile;
    private float atkTimer;
    public RaycastHit hit;
    public bool isAttackDelay;
    private float attackDelay;
    private float alpha;
    private int enemyLayer = 1 << 8;
    private int interactableLayer = 1 << 9;
    private int proIndex;
    private float r, g, b;
    public bool isSwitchColor;
    private float intensity;

    public ParticleSystem[] psSpell;
    public GameObject[] psCircle;
    private ParticleSystem.MainModule main;
    private bool magicUpgrade;
    public GameObject handIcon;


    // Start is called before the first frame update
    void Start()
    {
        atkTimer = 0.0f;
        isAttackDelay = false;
        isSwitchColor = false;
        magicUpgrade = false;
        attackDelay = 0.0f;
        alpha = 0.0f;
        intensity = 0;
        proIndex = 0;
        r = 1; g = 1; b = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerController.isDead)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 4, interactableLayer))
            {
                if (hit.collider.gameObject.tag == "Door" && !DoorControl.isLock)
                {
                    handIcon.SetActive(true);
                    if (Input.GetButtonDown("Interact"))
                    {
                        if (!hit.collider.gameObject.GetComponentInParent<DoorControl>().isOpen)
                        {
                            AudioManager.playDoorOpenSound = true;
                            hit.collider.gameObject.GetComponentInParent<DoorControl>().doorOpening = true;
                        }
                        else
                        {
                            AudioManager.playDoorCloseSound = true;
                            hit.collider.gameObject.GetComponentInParent<DoorControl>().doorClosing = true;
                        }
                    }

                }
                else if (hit.collider.gameObject.tag == "Key")
                {
                    handIcon.SetActive(true);
                    if (Input.GetButtonDown("Interact"))
                    {
                        AudioManager.playKeyCollectSound = true;
                        hit.collider.gameObject.GetComponent<KeyCollect>().Collect();
                    }
                }
                else if (hit.collider.gameObject.tag == "Puzzle" && !hit.collider.gameObject.GetComponentInParent<ActivePuzzle>().isComplete)
                {
                    handIcon.SetActive(true);
                    if (Input.GetButtonDown("Interact"))
                    {
                        AudioManager.playPuzzleActiveSound = true;
                        hit.collider.gameObject.GetComponentInParent<ActivePuzzle>().PuzzleControl(true);
                    }
                }
                else if (hit.collider.gameObject.tag == "Book")
                {
                    handIcon.SetActive(true);
                    if (Input.GetButtonDown("Interact"))
                    {
                        hit.collider.gameObject.GetComponent<SpellBookCollect>().Collect();
                    }
                }
            }
            else
            {
                handIcon.SetActive(false);
            }
            if (Input.GetButtonDown("Switch"))
            {
                if(WorldController.isEscapeMode)
                {
                    AudioManager.playChangeSpell = true;
                    if (proIndex == 1)
                    {
                        proIndex++;
                        r = 0.8f; g = 0.45f; b = 0.91f; //204,116,231 violet
                    }
                    else
                    {
                        proIndex--;
                        r = 0.38f; g = 0.83f; b = 0.87f; //97,212,221 blue
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        main = psSpell[i].main;
                        main.startColor = new Color(r, g, b, 1.0f);
                    }
                    this.gameObject.GetComponentInChildren<Light>().color = new Color(r, g, b, 1.0f);
                    isSwitchColor = true;
                    intensity = 0;
                    atkTimer = 3f;
                }  
            }

            if(isSwitchColor && !WorldController.isWin)
            {
                intensity += Time.deltaTime * 0.1f;
                this.gameObject.GetComponentInChildren<Light>().intensity = Mathf.Lerp(0, 6, intensity);
                if(intensity > 1)
                {
                    isSwitchColor = false;
                    intensity = 0;
                }
            }

            if(WorldController.isEscapeMode)
            {
                if(!magicUpgrade)
                {
                    r = 0.38f; g = 0.83f; b = 0.87f;
                    proIndex = 1;
                    magicUpgrade = true;
                    for (int i = 0; i < 2; i++)
                    {
                        main = psSpell[i].main;
                        main.startColor = new Color(r, g, b, 1.0f);
                    }
                    this.gameObject.GetComponentInChildren<Light>().color = new Color(r, g, b, 1.0f);
                    isSwitchColor = true;
                    intensity = 0;
                    atkTimer = 3f;
                }
            }

            if (isAttackDelay)
            {
                if (Physics.Raycast(transform.position, transform.forward, out hit, 40, enemyLayer)
                    || Physics.Raycast(transform.position - new Vector3(0.4f, 0, 0), transform.forward, out hit, 40, enemyLayer)
                    || Physics.Raycast(transform.position + new Vector3(0.4f, 0, 0), transform.forward, out hit, 40, enemyLayer)
                    || Physics.Raycast(transform.position - new Vector3(0, 0.4f, 0), transform.forward, out hit, 40, enemyLayer)
                    || Physics.Raycast(transform.position + new Vector3(0, 0.4f, 0), transform.forward, out hit, 40, enemyLayer))
                {
                    if (hit.collider.tag == "Enemy")
                    {
                        //Debug.Log(hit.collider.name);
                        hit.collider.GetComponentInParent<EnemyController>().magicType = proIndex;
                        if (proIndex == 2)
                        {
                            hit.collider.GetComponentInParent<EnemyController>().isSlow = true;
                            Debug.Log("Shoot");
                        }
                        else
                        {
                            hit.collider.GetComponentInParent<EnemyController>().isStun = true;
                        }
                    }
                    else if (hit.collider.tag =="Button")
                    {
                        hit.collider.GetComponentInParent<PlatformButton>().isHit = true;
                    }
                }
                attackDelay -= Time.deltaTime;
            }
            if (attackDelay <= 0)
            {
                isAttackDelay = false;
            }
            Debug.DrawRay(transform.position, transform.forward * 1000.0f, Color.red);
            if (atkTimer <= 0.0f && Input.GetButtonDown("Fire1") && Time.timeScale != 0)
            {
                PlayerController.isAttack = true;
                Instantiate(projectile[proIndex], transform.position, transform.rotation);
                isAttackDelay = true;
                attackDelay = 0.1f;
                atkTimer = 3.0f;
            }
            else if (atkTimer > 0.0f)
            {
                atkTimer -= Time.deltaTime;
                for(int i = 0; i < 2; i ++)
                {
                    main = psSpell[i].main;
                    main.startColor = new Color(r, g, b, (atkTimer / -3) + 1.0f);
                    psCircle[i].GetComponent<ParticleSystemRenderer>().maxParticleSize = (((atkTimer / -3) + 1.0f) * 0.2f);
                }

            }
        }
      
    }
}
