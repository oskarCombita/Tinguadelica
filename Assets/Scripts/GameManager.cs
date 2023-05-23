using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public enum LevelArea
{
    Test,
    Glitch,
    Snake
}

public class GameManager : MonoBehaviour
{
    public Button playStartBtn;

    public TextMeshProUGUI gameOverText;
    public Button restartBtn;

    public TextMeshProUGUI levelCompleteText;
    public Button playBtn;

    private BirdController birdController;
    private SpawnBadsManager spawnBads;
    private SpawnGoodsManager spawnGoods;
    private SpawnBgSkyManager spawnBgSky;
    private SpawnBackgroundManager spawnBgMountain;
    private SpawnBackgroundManager spawnBgHorizon;
    private SpawnBackgroundManager spawnBgShrubbery;

    public TextMeshProUGUI startText;

    public bool gameIsActive;
    public bool gameOver;
    public bool pause;

    public GameObject pauseScreen;
    public GameObject testObjects;

    public static float originalGravity;

    [SerializeField] private int mushToCompleteLevel;

    public LevelArea activeArea;

    public int mushToChangeArea;

    Dictionary<LevelArea, bool> areaSwitchDict = new Dictionary<LevelArea, bool>();

    void Start()
    {
        birdController = GameObject.Find("Bird").GetComponent<BirdController>();
        spawnBads = GameObject.Find("SpawnManager").GetComponent<SpawnBadsManager>();
        spawnGoods = GameObject.Find("SpawnManager").GetComponent<SpawnGoodsManager>();

        spawnBgSky = GameObject.Find("Bg_Sky_SpawnMger").GetComponent<SpawnBgSkyManager>();
        spawnBgMountain = GameObject.Find("Bg_Mountain_SpawnMger").GetComponent<SpawnBackgroundManager>();
        spawnBgHorizon = GameObject.Find("Bg_Horizon_SpawnMger").GetComponent<SpawnBackgroundManager>();
        spawnBgShrubbery = GameObject.Find("Bg_Shrubbery_SpawnMger").GetComponent<SpawnBackgroundManager>();

        gameOver = false;
        SetAreaSwitchDict();
        
        //StartGame();
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

    public void StartGame()
    {
        Destroy(testObjects);
        playStartBtn.gameObject.SetActive(false);
        //activeArea = LevelArea.Glitch;
        //spawnBads.SpawnBadsByArea(activeArea);
        gameIsActive = true;
        SwitchArea();

        spawnBgSky.StartSpawnSkyBG();
        spawnBgMountain.StartSpawnBG();
        spawnBgHorizon.StartSpawnBG();
        spawnBgShrubbery.StartSpawnBG();

        spawnGoods.StartSpawnGoods();

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
            case LevelArea.Test:
                activeArea = LevelArea.Glitch;
                break;

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
