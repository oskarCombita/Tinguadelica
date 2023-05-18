using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBadsManager : MonoBehaviour
{
    public GameObject sharkPrefab;
    private Vector2 spawnPos = new Vector2(11, -2.4f);
    private float startDelay = 2;
    private float repeatRate = 3;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        InvokeRepeating("SpawnShark", startDelay, repeatRate);
    }
 

    void SpawnShark()
    {
        if (!gameManager.gameOver)
        {
            Instantiate(sharkPrefab, spawnPos, sharkPrefab.transform.rotation);
        }        
    }
}
