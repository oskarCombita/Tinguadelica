using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveLeft : MonoBehaviour
{
    public float speed = 20;
    private float leftBound = -25;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (!gameManager.gameOver && gameManager.gameIsActive)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }        

        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
