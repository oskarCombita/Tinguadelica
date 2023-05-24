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
    Snake,
    Tunjo
}

public class GameManager : MonoBehaviour
{
    public Button playStartBtn;

    public TextMeshProUGUI gameOverText;
    public Button restartBtn;

    public TextMeshProUGUI levelCompleteText;
    public Button playBtn;

    public GameObject uiMushAGlitch;
    public TextMeshProUGUI uiMushAGlitchText;

    private BirdController birdController;
    private UiManager uiManager;
    private SpawnBadsManager spawnBads;
    private SpawnGoodsManager spawnGoods;
    private SpawnBgSkyManager spawnBgSky;
    private SpawnBackgroundManager spawnBgMountain;
    private SpawnBGHorizonManager spawnBgHorizon;
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

    private Transform imageMushGlitchA;
    private Transform imageMushSnakeA;

    Dictionary<LevelArea, bool> areaSwitchDict = new Dictionary<LevelArea, bool>();

    void Start()
    {
        birdController = GameObject.Find("Bird").GetComponent<BirdController>();
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>();
        spawnBads = GameObject.Find("SpawnManager").GetComponent<SpawnBadsManager>();
        spawnGoods = GameObject.Find("SpawnManager").GetComponent<SpawnGoodsManager>();

        spawnBgSky = GameObject.Find("Bg_Sky_SpawnMger").GetComponent<SpawnBgSkyManager>();
        spawnBgMountain = GameObject.Find("Bg_Mountain_SpawnMger").GetComponent<SpawnBackgroundManager>();
        spawnBgHorizon = GameObject.Find("Bg_Horizon_SpawnMger").GetComponent<SpawnBGHorizonManager>();
        spawnBgShrubbery = GameObject.Find("Bg_Shrubbery_SpawnMger").GetComponent<SpawnBackgroundManager>();

        gameOver = false;
        SetAreaSwitchDict();

        imageMushGlitchA = startText.transform.GetChild(0);
        imageMushSnakeA = startText.transform.GetChild(1);
        
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

    void SetAreaSwitchDict()
    {
        foreach (LevelArea area in Enum.GetValues(typeof(LevelArea)))
        {
            areaSwitchDict.Add(area, false);
        }
    }

    public void StartGame()
    {
        Destroy(testObjects);
        playStartBtn.gameObject.SetActive(false);
        gameIsActive = true;
        SwitchArea();

        spawnBgSky.StartSpawnSkyBG();
        spawnBgMountain.StartSpawnBG();
        spawnBgHorizon.StartSpawnBG();
        spawnBgShrubbery.StartSpawnBG();

        spawnGoods.StartSpawnGoods();

        imageMushGlitchA.gameObject.SetActive(true);
        startText.text = "Recoge " + mushToChangeArea + " hongos";
        StartCoroutine(TurnOffStartText(3));
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
            areaSwitchDict[activeArea] = true;
            SwitchArea();            
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
                StartSnakeArea();               
                break;

            case LevelArea.Snake:
                activeArea = LevelArea.Tunjo;
                StartTunjoArea();
                break;
        }

        SetMushCountAtSwitchArea();  

        spawnBads.SpawnBadsByArea(activeArea);
    }

    void SetMushCountAtSwitchArea()
    {
        if (birdController.pickedMush >= mushToChangeArea)
        {
            birdController.pickedMush = birdController.pickedMush - mushToChangeArea;
        }
        else
        {
            birdController.pickedMush = 0;
        }

        uiManager.AnimMushroomCanvas();
        uiManager.UpdateMushroomUiCount();
    }

    void StartSnakeArea()
    {
        uiMushAGlitchText.text = "x " + mushToChangeArea;
        uiMushAGlitch.gameObject.SetActive(true);

        imageMushSnakeA.gameObject.SetActive(true);
        startText.gameObject.SetActive(true);
        StartCoroutine(TurnOffStartText(3));
        Debug.Log("Area: " + activeArea + " Bool: " + areaSwitchDict[activeArea]);
    }

    void StartTunjoArea()
    {
        startText.text = "Tunjo Area";
        startText.gameObject.SetActive(true);
        StartCoroutine(TurnOffStartText(3));
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
        spawnGoods.spriteRendererMush.color = spawnGoods.originalMushColor;
        spawnGoods.spriteRendererMushFly.color = spawnGoods.originalMushColor;
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
        spawnGoods.spriteRendererMush.color = spawnGoods.originalMushColor;
        spawnGoods.spriteRendererMushFly.color = spawnGoods.originalMushColor;
        playBtn.gameObject.SetActive(true);
        levelCompleteText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Physics2D.gravity = new Vector2(0f, originalGravity);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
