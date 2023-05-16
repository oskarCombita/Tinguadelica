using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBadsManager : MonoBehaviour
{
    public GameObject sharkPrefab;
    private Vector2 spawnPos = new Vector2(11, -2.4f);
    private float startDelay = 2;
    private float repeatRate = 4;

    void Start()
    {
        InvokeRepeating("SpawnShark", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnShark()
    {
        Instantiate(sharkPrefab, spawnPos, sharkPrefab.transform.rotation);
    }
}
