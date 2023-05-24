using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGoodsManager : MonoBehaviour
{
    public GameObject[] goodsPrefabs;
    public GameObject[] ShadowPrefs;
    public GameObject flyHeartPref;

    private float startDelayFlyH = 7;
    private float repeatRateFlyH = 21;
    private float startDelay = 2;
    private float repeatRate = 2;

    private GameManager gameManager;

    private Color currentMushColor;
    public Color originalMushColor;
    [SerializeField] private Color mushColorASnake;    
    [HideInInspector] public SpriteRenderer spriteRendererMush;
    [HideInInspector] public SpriteRenderer spriteRendererMushFly;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spriteRendererMush = goodsPrefabs[0].GetComponent<SpriteRenderer>();
        spriteRendererMushFly = goodsPrefabs[3].GetComponentInChildren<SpriteRenderer>();
        currentMushColor = originalMushColor;
        spriteRendererMush.color = originalMushColor;
        spriteRendererMushFly.color = originalMushColor;
    }

    void Update()
    {
        SetMushColor(gameManager.activeArea);
    }

    public void StartSpawnGoods()
    {
        InvokeRepeating("SpawnGoods", startDelay, repeatRate);
        InvokeRepeating("SpawnFlyHeart", startDelayFlyH, repeatRateFlyH);
    }

    void SpawnGoods()
    {
        int goodsIndex = Random.Range(0, goodsPrefabs.Length);
        Vector2 spawnPos = Vector2.zero;

        if (goodsIndex == 0) // Mushroom Run
        {
            spawnPos = new Vector2(11, -2.6f);
            spriteRendererMush.color = currentMushColor;
        }
        else if (goodsIndex == 1) // Mushroom
        {
            spawnPos = new Vector2(11, 0.26f);
        } 
        else if(goodsIndex == 2) // Heart
        {
            float randomYPos = Random.Range(1f, 9f);
            spawnPos = new Vector2(11, randomYPos);

            Vector2 spawnShadowPos = new Vector2(9.7f, -3.2f);
            Instantiate(ShadowPrefs[0], spawnShadowPos, ShadowPrefs[0].transform.rotation);
        }
        else // Fly Mushroom
        {
            float randomYPos = Random.Range(5f, 9.5f);
            spawnPos = new Vector2(15, randomYPos);
            spriteRendererMushFly.color = currentMushColor;

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
            Instantiate(goodsPrefabs[goodsIndex], spawnPos, goodsPrefabs[goodsIndex].transform.rotation);
        }        
    }

    void SpawnFlyHeart()
    {
        if (!gameManager.gameOver)
        {
            float randomYPos = Random.Range(2f, 9f);
            Vector2 spawnFlyPos = new Vector2(11, randomYPos);
            Instantiate(flyHeartPref, spawnFlyPos, flyHeartPref.transform.rotation);

            Vector2 spawnShadowPos = new Vector2(9.7f, -3.2f);
            Instantiate(ShadowPrefs[1], spawnShadowPos, ShadowPrefs[1].transform.rotation);
        }        
    }

    void SetMushColor(LevelArea area)
    {
        switch (area)
        {
            case LevelArea.Test:
                currentMushColor = originalMushColor;
                break;

            case LevelArea.Glitch:
                currentMushColor = originalMushColor;
                break;

            case LevelArea.Snake:
                currentMushColor = mushColorASnake;
                break;           
        }
    }
}
