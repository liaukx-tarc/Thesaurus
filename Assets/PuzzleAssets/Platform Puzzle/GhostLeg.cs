using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GhostLeg : MonoBehaviour
{
	public int col;
    public int row;
    public int minFoot;
    public int maxFoot;
    public int moveSpeed;
    public bool isActivate;
    public GameObject footPrefab;
    public GameObject goalPrefab;
    public GameObject numField;
    public GameObject buttonPrefab;
    public GameObject controlPlatformLeft;
    public GameObject controlPlatformRight;

    bool[][] footArr;
    public int targetNum, ansNum;
	// Start is called before the first frame update

    void Start()
	{
        isActivate = false;

        SpawnFoot();
        targetNum = Random.Range(0, col);
        Instantiate(goalPrefab, new Vector3(this.transform.position.x - 0.4f * (targetNum - 2),
                    this.transform.position.y - 0.4f * (row - 2) - 0.2f,
                    this.transform.position.z), Quaternion.identity, gameObject.transform);
        WriteNum();

        FindGoal();
        minFoot = row / 3;
        maxFoot = row / 2;

        ButtonSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(isActivate)
        {
            if (controlPlatformLeft.transform.localPosition.y <= 0 && controlPlatformRight.transform.localPosition.y <= 0)
            {
                StartCoroutine(ResetPlatform());
                isActivate = false;
            }
            else
            {
                StartCoroutine(ActivatePlatform());
            }
        }
    }

    void SpawnFoot()
    {
        //create a array based on the number we want
        footArr = new bool[row][];
        for (int i = 0; i < row; ++i)
            footArr[i] = new bool[col];

        //Reset array
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                footArr[i][j] = false;
            }
        }

        //Generate the foot
        for (int i = 0; i < col; i++)
        {
            int footNum = Random.Range(row / 3, row / 2);
            for (int j = 0; j <= footNum; j++)
            {
                //randam a row
                int rowNum = Random.Range(0, row - 1);

                //check the it not bigger col
                int rightCol = i;
                if (i != col - 1)
                {
                    rightCol++;
                }

                //check the it not smaller col
                int leftCol = i;
                if (i != 0)
                {
                    leftCol--;
                }

                bool addCheck;
                //check the left and rigt of the choosed row not have foot
                do
                {
                    addCheck = false;
                    if (footArr[rowNum][leftCol] == false && footArr[rowNum][rightCol] == false)
                    {
                        footArr[rowNum][i] = true;
                        addCheck = true;
                    }
                    else if (rowNum < row - 1)
                    {
                        rowNum++;
                    }
                    else
                    {
                        rowNum = 0;
                    }

                } while (addCheck == false);
            }
        }

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                Instantiate(footPrefab, new Vector3(this.transform.position.x - 0.4f * (j - 2),
                    this.transform.position.y - 0.4f * (i - 2),
                    this.transform.position.z), Quaternion.Euler(0, 0, 90), gameObject.transform);

                if (footArr[i][j])
                {
                    GameObject obj = Instantiate(footPrefab, new Vector3(this.transform.position.x - (0.4f * (j - 1) - 0.15f),
                        this.transform.position.y - 0.4f * (i - 2),
                        this.transform.position.z), Quaternion.identity, gameObject.transform);
                    obj.name = "Foot_Horizon";
                }

            }

            Instantiate(footPrefab, new Vector3(this.transform.position.x - 0.4f * (col - 2),
                    this.transform.position.y - 0.4f * (i - 2),
                    this.transform.position.z), Quaternion.Euler(0, 0, 90), gameObject.transform);
        }
    }

    void FindGoal()
    {
        ansNum = targetNum;
        for (int j = row - 1; j >= 0; j--)
        {
            bool changeCheck = false;
            if (ansNum - 1 >= 0)
            {
                if (footArr[j][ansNum - 1] == true)
                {
                    ansNum--;
                    changeCheck = true;
                }
            }

            if (ansNum < col && !changeCheck)
            {
                if (footArr[j][ansNum] == true)
                {
                    ansNum++;
                }
            }
        }
    }

    string RomanNum(int num)
    {
        string x = "x", ix = "ix", v = "v", iv = "iv", i = "i";
        string romanNum = "";

        while (num != 0)
        {
            if (num >= 10)    // 10 - x
            {
                romanNum += x;
                num -= 10;
            }

            else if (num >= 9)     // 9 - ix
            {
                romanNum += ix;
                num -= 9;
            }

            else if (num >= 5)     // 5 - v
            {
                romanNum += v;
                num -= 5;
            }

            else if (num >= 4)     // 4 - iv
            {
                romanNum += iv;
                num -= 4;
            }

            else if (num >= 1)     // 1 - i
            {
                romanNum += i;
                num -= 1;
            }
        }
                
        return romanNum;
    }

    void WriteNum()
    {
        for (int i = 0; i <= col; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, new Vector3(this.transform.position.x - 0.4f * (i - 2),
                   this.transform.position.y + 0.5f * 3,
                   this.transform.position.z), Quaternion.identity, gameObject.transform);

            newButton.GetComponentInChildren<TextMeshPro>().text = RomanNum(i + 1);
            newButton.GetComponent<PlatformButton>().buttonNum = i;
        }
        
    }

    void ButtonSpawn()
    {
        for (int i = 0; i <= col; i++)
        {
            float positionX = 0;

            if ((col + 1) % 4 == 2)
            {
                if (i / 4 == (col + 1) / 4)
                {
                    positionX = 1.5f;
                }

            }

            if ((col + 1) % 4 == 3)
            {
                if (i / 4 == (col + 1) / 4)
                {
                    positionX = 0.75f;
                }
            }

            
        }
       
    }

    IEnumerator ActivatePlatform()
    {
        yield return new WaitForSeconds(0.05f);
        controlPlatformLeft.transform.position = controlPlatformLeft.transform.position + new Vector3(0, -moveSpeed * Time.deltaTime, 0);
        controlPlatformRight.transform.position = controlPlatformRight.transform.position + new Vector3(0, -moveSpeed * Time.deltaTime, 0);
    }

    IEnumerator ResetPlatform()
    {
        yield return new WaitForSeconds(0.05f);
        controlPlatformLeft.transform.localPosition = new Vector3(controlPlatformLeft.transform.localPosition.x,
                    0.0f, controlPlatformLeft.transform.localPosition.z);
        controlPlatformRight.transform.localPosition = new Vector3(controlPlatformRight.transform.localPosition.x,
            0.0f, controlPlatformRight.transform.localPosition.z);
    }
}


