using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public enum LevelArea
{
    Glitch,
    Snake
}

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public Button restartBtn;

    public TextMeshProUGUI levelCompleteText;
    public Button playBtn;

    private BirdController birdController;
    private SpawnBadsManager spawnBads;

    public TextMeshProUGUI startText;

    public bool gameOver;
    public bool pause;

    public GameObject pauseScreen;

    public static float originalGravity;

    [SerializeField] private int mushToCompleteLevel;

    public LevelArea activeArea;

    public int mushToChangeArea;

    Dictionary<LevelArea, bool> areaSwitchDict = new Dictionary<LevelArea, bool>();

    void Start()
    {
        birdController = GameObject.Find("Bird").GetComponent<BirdController>();
        spawnBads = GameObject.Find("SpawnManager").GetComponent<SpawnBadsManager>(); 
        
        gameOver = false;
        SetAreaSwitchDict();
        StartGame();
    }
    
    void Update()
    {
        EndGame();
        AreaManager();

        if (Input.GetKeyDown(KeyCode.P) && !gameOver)
        {
            SetPause();
        }
    }

    void StartGame()
    {
        activeArea = LevelArea.Glitch;
        spawnBads.SpawnBadsByArea(activeArea);
        startText.text = "Recoge " + mushToCompleteLevel + " hongos";
        StartCoroutine(TurnOffStartText(3));
    }

    void SetAreaSwitchDict()
    {
        foreach (LevelArea area in Enum.GetValues(typeof(LevelArea)))
        {
            areaSwitchDict.Add(area, false);
        }
    }

    IEnumerator TurnOffStartText(float delay)
    {
        yield return new WaitForSeconds(delay);
        startText.gameObject.SetActive(false);
    }

    void AreaManager()
    {
        if (birdController.pickedMush >= mushToChangeArea && !areaSwitchDict[activeArea])
        {
            SwitchArea();
            areaSwitchDict[activeArea] = true;
        }        
    }

    void SwitchArea()
    {
        switch (activeArea)
        {
            case LevelArea.Glitch:
                activeArea = LevelArea.Snake;
                break;
        }

        spawnBads.SpawnBadsByArea(activeArea);
    }

    void EndGame()
    {
        if (birdController.live <= 0 && !gameOver)
        {
            gameOver = true;
            GameOver();
        }

        if (birdController.pickedMush >= mushToCompleteLevel)
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
    }

    public void RestartGame()
    {
        Physics2D.gravity = new Vector2(0f, originalGravity);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
