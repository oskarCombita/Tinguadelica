using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBGHorizonManager : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 secondPos = new Vector2(19.2f, 0);

    private Vector2 flyHolePos = new Vector2(4.3f, 5.5f);

    private float destroyPos = 19.2f;

    public GameObject[] backgroundPrefabs;
    public GameObject flyHoleShadow;
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

            if (bGIndex == 3)
            {
                Instantiate(flyHoleShadow, flyHolePos + secondPos, Quaternion.identity);
                MoveLeft moveLeft = backgroundPrefabs[bGIndex].GetComponent<MoveLeft>();
                moveLeft.isBonusBg = true;
            }
        }
        else if(transform.position.x < startPos.x - destroyPos)
        {
            transform.position = startPos;
            int bGIndex = Random.Range(0, backgroundPrefabs.Length);
            Instantiate(backgroundPrefabs[bGIndex], startPos + secondPos, backgroundPrefabs[bGIndex].transform.rotation);

            if (bGIndex == 3)
            {
                Instantiate(flyHoleShadow, flyHolePos + secondPos, Quaternion.identity);
                MoveLeft moveLeft = backgroundPrefabs[bGIndex].GetComponent<MoveLeft>();
                moveLeft.isBonusBg = true;
            }
        }
    }

    public void StartSpawnBG()
    {
        int bGIndex = Random.Range(0, backgroundPrefabs.Length - 1);
        Instantiate(backgroundPrefabs[bGIndex], startPos, backgroundPrefabs[bGIndex].transform.rotation);

        if (bGIndex == 3)
        {
            Instantiate(flyHoleShadow, flyHolePos, Quaternion.identity);
            MoveLeft moveLeft = backgroundPrefabs[bGIndex].GetComponent<MoveLeft>();
            moveLeft.isBonusBg = true;
        }

        int bGIndex2 = Random.Range(0, backgroundPrefabs.Length - 1);
        Instantiate(backgroundPrefabs[bGIndex2], startPos + secondPos, backgroundPrefabs[bGIndex2].transform.rotation);

        if (bGIndex2 == 3)
        {
            Instantiate(flyHoleShadow, flyHolePos + secondPos, Quaternion.identity);
            MoveLeft moveLeft = backgroundPrefabs[bGIndex].GetComponent<MoveLeft>();
            moveLeft.isBonusBg = true;
        }
    }
}
