using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostLegAns : MonoBehaviour
{
    public int targetNum, row, col;
    public float lineLenght, moveSpeed, intiLenght;
    public bool[][] footArr;
    public bool test;

    int currentRow, currentCol;
    float lenghtNeedMove;
    bool isHorizontalMoving;
    Vector2 moveDirection;

    private void Start()
    {
        currentCol = targetNum;
        currentRow = -1;
        isHorizontalMoving = false;
        lenghtNeedMove = intiLenght;
    }

    private void FixedUpdate()
    {
        if (lenghtNeedMove <= 0)
        {
            if (!isHorizontalMoving)
            {
                if (currentRow >= row - 1)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    currentRow++;
                    CheckFoot();
                } 
            }

            else
            {
                if (currentRow == row)
                {
                    Destroy(this.gameObject);
                }
                lenghtNeedMove = lineLenght;
                moveDirection.x = 0;
                moveDirection.y = -1;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                isHorizontalMoving = false;
            }   
        }

        else
        {
            this.transform.localPosition +=
                new Vector3(moveSpeed * moveDirection.x,
                moveSpeed * moveDirection.y, 0);

            lenghtNeedMove -= moveSpeed;
        }
    }

    public void inti(int targetNum, int row, int col, bool[][] footArr)
    {
        this.targetNum = targetNum;
        this.row = row;
        this.col = col;
        this.footArr = footArr;

        moveDirection.x = 0;
        moveDirection.y = -1;
    }

    public void CheckFoot()
    {
        bool leftCheck = false;

        if (currentCol - 1 >= 0)
        {
            if (footArr[currentRow][currentCol - 1] == true)
            {   
                currentCol--;
                moveDirection.x = -1;
                moveDirection.y = 0;
                
                leftCheck = true; //Tell left side have foot
                isHorizontalMoving = true;
                transform.localRotation = Quaternion.Euler(0, 0, -90);
            }
        }

        if (currentCol < col && !leftCheck)
        {
            if (footArr[currentRow][currentCol] == true)
            {
                currentCol++;
                moveDirection.x = 1;
                moveDirection.y = 0;

                isHorizontalMoving = true;
                transform.localRotation = Quaternion.Euler(0, 0, 90);
            }
        }

        lenghtNeedMove = lineLenght;
    }
}
