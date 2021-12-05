using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHouse : MonoBehaviour
{
    public int itemNum;
    public GameObject[] houseList;

    // Start is called before the first frame update
    void Start()
    {
        List<int> selectedHouse = new List<int>();
        bool isSame;

        if(itemNum > houseList.Length)
        {
            itemNum = houseList.Length;
        }

        for (int i = 0; i < itemNum; i++)
        {
            do
            {
                int houseNum = Random.RandomRange(0, houseList.Length - 1);
                isSame = false;

                for (int j = 0; j < selectedHouse.Count; j++)
                {
                    if (houseNum == selectedHouse[j])
                    {
                        isSame = true;
                    }
                }

                if (isSame == false)
                {
                    selectedHouse.Add(houseNum);
                    houseList[houseNum].GetComponent<RandomItem>().SpawnItem();
                }

            } while (isSame);
            
        }
        
    }
}
