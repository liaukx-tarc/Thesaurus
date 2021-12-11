using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    public static int itemNum;
    public GameObject[] spawnPoint;
    public GameObject[] spawnItem;

    public void Start()
    {
        itemNum = 0;
    }

    public void SpawnItem()
    {
        int pointNum = Random.Range(0, spawnPoint.Length - 1);

        Instantiate(spawnItem[itemNum], spawnPoint[pointNum].transform);
        itemNum++;
    }
}
