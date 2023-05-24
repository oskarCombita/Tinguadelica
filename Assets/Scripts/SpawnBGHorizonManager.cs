using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBGHorizonManager : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 secondPos = new Vector2(19.2f, 0);

    private float destroyPos = 19.2f;

    public GameObject[] backgroundPrefabs;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        startPos = transform.position;
    }

    void Update()
    {
        if (transform.position.x < startPos.x - destroyPos && gameManager.activeArea != LevelArea.Snake)
        {
            transform.position = startPos;
            int bGIndex = Random.Range(0, backgroundPrefabs.Length - 1);
            Instantiate(backgroundPrefabs[bGIndex], startPos + secondPos, backgroundPrefabs[bGIndex].transform.rotation);
        }
        else if(transform.position.x < startPos.x - destroyPos)
        {
            transform.position = startPos;
            int bGIndex = Random.Range(0, backgroundPrefabs.Length);
            Instantiate(backgroundPrefabs[bGIndex], startPos + secondPos, backgroundPrefabs[bGIndex].transform.rotation);
        }
    }

    public void StartSpawnBG()
    {
        int bGIndex = Random.Range(0, backgroundPrefabs.Length - 1);
        Instantiate(backgroundPrefabs[bGIndex], startPos, backgroundPrefabs[bGIndex].transform.rotation);

        int bGIndex2 = Random.Range(0, backgroundPrefabs.Length - 1);
        Instantiate(backgroundPrefabs[bGIndex2], startPos + secondPos, backgroundPrefabs[bGIndex2].transform.rotation);
    }

    public void StartSpawnBgAreaSnake()
    {
        int bGIndex = Random.Range(1, backgroundPrefabs.Length);
        Instantiate(backgroundPrefabs[bGIndex], startPos, backgroundPrefabs[bGIndex].transform.rotation);

        int bGIndex2 = Random.Range(1, backgroundPrefabs.Length);
        Instantiate(backgroundPrefabs[bGIndex2], startPos + secondPos, backgroundPrefabs[bGIndex2].transform.rotation);
    }
}