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

    public TextMeshProUGUI levelCompleteText;
    public Button playBtn;

    private BirdController birdController;
    private UiManager uiManager;

    public TextMeshProUGUI startText;

    public bool gameOver;
    public bool pause;

    public GameObject pauseScreen;

    public static float originalGravity;

    [SerializeField] private int mushToComplete;


    void Start()
    {
        birdController = GameObject.Find("Bird").GetComponent<BirdController>();
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>();
        
        gameOver = false;
        StartGame();
    }
    
    void Update()
    {
        EndGame();

        if (Input.GetKeyDown(KeyCode.P) && !gameOver)
        {
            SetPause();
        }
    }

    void StartGame()
    {
        startText.text = "Recoge " + mushToComplete + " hongos";
        StartCoroutine(TurnOffStartText(3));
    }

    IEnumerator TurnOffStartText(float delay)
    {
        yield return new WaitForSeconds(delay);
        startText.gameObject.SetActive(false);
    }

    void EndGame()
    {
        if (birdController.live <= 0 && !gameOver)
        {
            gameOver = true;
            GameOver();
        }

        if (uiManager.countMushrooms == mushToComplete)
        {
            gameOver = true;
            LevelComplete();
        }
    }

    public void GameOver()
    {
        restartBtn.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);

        Debug.Log("GameOver");
    }

    void SetPause()
    {
        if (!pause)
        {
            pause = !pause;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pause = !pause;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void LevelComplete()
    {
        playBtn.gameObject.SetActive(true);
        levelCompleteText.gameObject.SetActive(true);

        Debug.Log("Level Complete");
    }

    public void RestartGame()
    {
        Physics2D.gravity = new Vector2(0f, originalGravity);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
