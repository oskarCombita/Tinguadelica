using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBadsManager : MonoBehaviour
{
    public GameObject glitchPrefab;
    public GameObject snakePrefab;
    private Vector2 spawnPos = new Vector2(11, -2.4f);
    private float startDelay = 3;
    private float repeatRateGlitch = 3;

    [SerializeField] private float repeatRateSnake;

    private GameManager gameManager;
    

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();              
    }   
    
    public void SpawnBadsByArea(LevelArea area)
    {
        switch (area)
        {
            case LevelArea.Glitch:
                InvokeRepeating("SpawnGlitch", startDelay, repeatRateGlitch);
                break;

            case LevelArea.Snake:
                CancelInvoke("SpawnGlitch");
                InvokeRepeating("SpawnSnake", startDelay, repeatRateSnake);                
                break;

            case LevelArea.Tunjo:
                CancelInvoke("SpawnSnake");
                InvokeRepeating("SpawnGlitch", startDelay, repeatRateGlitch);
                break;
        }
    }

    void SpawnSnake()
    {
        if (!gameManager.gameOver)
        {
            Instantiate(snakePrefab, new Vector2(5, 2), Quaternion.identity);
        }        
    }


    void SpawnGlitch()
    {
        if (!gameManager.gameOver)
        {
            Instantiate(glitchPrefab, spawnPos, glitchPrefab.transform.rotation);
        }        
    }
}
