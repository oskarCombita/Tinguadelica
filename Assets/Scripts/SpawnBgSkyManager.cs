using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBgSkyManager : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 secondPos = new Vector2(19.2f, 0);

    private float destroyPos = 19.2f;

    public GameObject backgroundPrefab;

    void Start()
    {
        startPos = transform.position;
    }
    
    void Update()
    {
        if (transform.position.x < startPos.x - destroyPos)
        {
            transform.position = startPos;            
            Instantiate(backgroundPrefab, startPos + secondPos, backgroundPrefab.transform.rotation);
        }
    }

    public void StartSpawnSkyBG()
    {
        Instantiate(backgroundPrefab, startPos, backgroundPrefab.transform.rotation);

        Instantiate(backgroundPrefab, startPos + secondPos, backgroundPrefab.transform.rotation);
    }
}
