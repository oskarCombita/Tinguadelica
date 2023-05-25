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
    public GameObject uiMushASnake;
    public GameObject uiMushATunjo;

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

    public int mushToCompleteLevel;

    public LevelArea activeArea;

    public int mushToChangeArea;

    private Transform imageMushGlitchA;
    private Transform imageMushSnakeA;
    private Transform imageMushTunjoA;

    public Image jumpIcon;

    Dictionary<LevelArea, bool> areaSwitchDict = new Dictionary<LevelArea, bool>();

    private AudioSource gameManagerAudioSource;
    AudioClip playTheme = null;
    public AudioClip levelOneThemeSong;
    
    private void Awake()
    {
        gameManagerAudioSource = GetComponent<AudioSource>();
    } 

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
        imageMushTunjoA = startText.transform.GetChild(2);
        
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
        jumpIcon.gameObject.SetActive(false);
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
        PlayThemeSong();
        StartCoroutine(TurnOffStartText(3));
    }

    IEnumerator TurnOffStartText(float delay)
    {
        yield return new WaitForSeconds(delay);
        startText.gameObject.SetActive(false);
    }

    void AreaManager()
    {
        if (birdController.pickedMush >= mushToChangeArea && !areaSwitchDict[activeArea] && activeArea != LevelArea.Tunjo)
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
        birdController.pickedMush = 0;

        uiManager.AnimMushroomCanvas();
        uiManager.UpdateMushroomUiCount();        
    }

    void StartSnakeArea()
    {
        uiManager.mushCountImage.sprite = uiManager.mushSnake;
        uiMushAGlitch.gameObject.SetActive(true);
        Invoke("ShowStartAreaInfo", 1.4f);
    }

    void StartTunjoArea()
    {
        uiManager.mushCountImage.sprite = uiManager.mushTunjo;
        uiMushASnake.gameObject.SetActive(true);
        Invoke("ShowStartAreaInfo", 1.4f);
    }

    void ShowStartAreaInfo()
    {
        if (activeArea == LevelArea.Snake)
        {
            imageMushSnakeA.gameObject.SetActive(true);
        }
        else
        {
            imageMushTunjoA.gameObject.SetActive(true);
            startText.text = "Recoge " + mushToCompleteLevel + " hongos";
        }
        
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

        if (birdController.pickedMush >= mushToCompleteLevel && !gameOver)
        {
            gameOver = true;
            LevelComplete();
        }
    }

    public void GameOver()
    {
        spawnGoods.spriteRendererMush.color = spawnGoods.originalMushColor;
        spawnGoods.spriteRendererMushFly.color = spawnGoods.originalMushColor;

        startText.gameObject.SetActive(false);

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
        uiMushATunjo.gameObject.SetActive(true);

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

    void PlayThemeSong()
    {
        playTheme = levelOneThemeSong;
        gameManagerAudioSource.clip = playTheme;
        gameManagerAudioSource.Play();
    }

}
