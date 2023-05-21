using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGoodsManager : MonoBehaviour
{
    public GameObject[] goodsPrefab;
    public GameObject[] ShadowPrefs;
    public GameObject flyHeartPref;
    private float startDelayFlyH = 7;
    private float repeatRateFlyH = 21;
    private float startDelay = 2;
    private float repeatRate = 2;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        InvokeRepeating("SpawnGoods", startDelay, repeatRate);
        InvokeRepeating("SpawnFlyHeart", startDelayFlyH, repeatRateFlyH);
    }


    void SpawnGoods()
    {
        int goodsIndex = Random.Range(0, goodsPrefab.Length);
        Vector2 spawnPos = Vector2.zero;

        if (goodsIndex == 0)
        {
            spawnPos = new Vector2(11, -2.6f);
        }
        else if (goodsIndex == 1)
        {
            spawnPos = new Vector2(11, 0.26f);
        } 
        else if(goodsIndex == 2)
        {
            float randomYPos = Random.Range(1f, 9f);
            spawnPos = new Vector2(11, randomYPos);

            Vector2 spawnShadowPos = new Vector2(9.7f, -3.2f);
            Instantiate(ShadowPrefs[0], spawnShadowPos, ShadowPrefs[0].transform.rotation);
        }
        else
        {
            float randomYPos = Random.Range(5f, 10f);
            spawnPos = new Vector2(15, randomYPos);

            Vector2 spawnShadowPos = Vector2.zero;
            if (randomYPos < 6f)
            {
                spawnShadowPos = new Vector2(13f, -3.2f);
            }else if (randomYPos < 8)
            {
                spawnShadowPos = new Vector2(11f, -3.2f);
            }else 
            {
                spawnShadowPos = new Vector2(10f, -3.2f);
            }
            Instantiate(ShadowPrefs[2], spawnShadowPos, ShadowPrefs[2].transform.rotation);
        }

        if (!gameManager.gameOver)
        {
            Instantiate(goodsPrefab[goodsIndex], spawnPos, goodsPrefab[goodsIndex].transform.rotation);
        }        
    }

    void SpawnFlyHeart()
    {
        float randomYPos = Random.Range(2f, 9f);
        Vector2 spawnFlyPos = new Vector2(11, randomYPos);
        Instantiate(flyHeartPref, spawnFlyPos, flyHeartPref.transform.rotation);

        Vector2 spawnShadowPos = new Vector2(9.7f, -3.2f);
        Instantiate(ShadowPrefs[1], spawnShadowPos, ShadowPrefs[1].transform.rotation);
    }
}
