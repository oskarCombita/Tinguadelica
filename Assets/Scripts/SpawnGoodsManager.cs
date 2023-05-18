using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGoodsManager : MonoBehaviour
{
    public GameObject[] goodsPrefab;
    public GameObject heartShadowPref;
    private float startDelay = 2;
    private float repeatRate = 2;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        InvokeRepeating("SpawnMushroom", startDelay, repeatRate);
    }


    void SpawnMushroom()
    {
        int mushroomIndex = Random.Range(0, goodsPrefab.Length);
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

            Vector2 spawnShadowPos = new Vector2(11, -3.2f);
            Instantiate(heartShadowPref, spawnShadowPos, heartShadowPref.transform.rotation);
        }

        if (!gameManager.gameOver)
        {
            Instantiate(goodsPrefab[mushroomIndex], spawnPos, goodsPrefab[mushroomIndex].transform.rotation);
        }        
    }
}
