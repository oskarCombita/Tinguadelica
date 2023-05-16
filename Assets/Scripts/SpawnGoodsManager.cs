using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGoodsManager : MonoBehaviour
{
    public GameObject[] mushroomPrefab;
    private float startDelay = 2;
    private float repeatRate = 2;

    void Start()
    {
        InvokeRepeating("SpawnMushroom", startDelay, repeatRate);
    }


    void SpawnMushroom()
    {
        int mushroomIndex = Random.Range(0, mushroomPrefab.Length);
        Vector2 spawnPos = Vector2.zero;

        if (mushroomIndex == 0)
        {
            spawnPos = new Vector2(11, -2.6f);
        }
        else if (mushroomIndex == 1)
        {
            spawnPos = new Vector2(11, 0.26f);
        } 
        else
        {
            float randomYPos = Random.Range(1f, 9f);
            spawnPos = new Vector2(11, randomYPos);        
        }

        Instantiate(mushroomPrefab[mushroomIndex], spawnPos, mushroomPrefab[mushroomIndex].transform.rotation);
    }
}
