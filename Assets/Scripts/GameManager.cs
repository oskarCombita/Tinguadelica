using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public Button restartBtn;

    private BirdController birdController;
    public bool gameOver;

    public static float originalGravity;


    void Start()
    {
        birdController = GameObject.Find("Bird").GetComponent<BirdController>();
        gameOver = false;
    }
    
    void Update()
    {
        EndGame();
    }

    void EndGame()
    {
        if (birdController.live <= 0 && !gameOver)
        {
            gameOver = true;
            GameOver();
        }
    }

    public void GameOver()
    {
        restartBtn.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);

        Debug.Log("GameOver");
    }

    public void RestartGame()
    {
        Physics2D.gravity = new Vector2(0f, originalGravity);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
