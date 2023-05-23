using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoBoogerSpawn : MonoBehaviour
{
    public GameObject ufoBoogerPrefab;
    private GameManager gameManager;
    Vector2 spawnPos = Vector2.zero;
    private float startDelay = 3;
    private float repeatRate = 3;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        InvokeRepeating("SpawnUfoBooger", startDelay, repeatRate);
    }

    void SpawnUfoBooger(){
        spawnPos = new Vector2(19, 4);
        if (!gameManager.gameOver)
        {
            Instantiate(ufoBoogerPrefab, spawnPos, ufoBoogerPrefab.transform.rotation);
        } 
    }
}
