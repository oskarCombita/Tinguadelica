using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpermeyeSpawn : MonoBehaviour
{
    public GameObject spermeyePrefab;
    private GameManager gameManager;
    Vector2 spawnPos = Vector2.zero;
    private float startDelay = 3;
    private float repeatRate = 3;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        InvokeRepeating("SpawnSpermeye", startDelay, repeatRate);
    }

    void SpawnSpermeye(){
        spawnPos = new Vector2(19, -2.6f);
        if (!gameManager.gameOver)
        {
            Instantiate(spermeyePrefab, spawnPos, spermeyePrefab.transform.rotation);
        } 
    }
}
