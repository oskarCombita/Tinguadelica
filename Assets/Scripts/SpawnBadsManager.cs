using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBadsManager : MonoBehaviour
{
    public GameObject glitchPrefab;
    public GameObject snakePrefab;
    public GameObject tunjoPrefab;
    private Vector2 spawnGlitchPos = new Vector2(11, -2.4f);
    private float startDelay = 4;
    private float repeatRateGlitch = 3;

    [SerializeField] private float repeatRateSnake;

    private GameManager gameManager;

    AudioClip playGlitchSound = null;
    private AudioSource enemyAudioSource;
    public AudioClip glitchSound;

    AudioClip playSnakeSound = null;
    public AudioClip snakeSound;

    AudioClip playTunjoSound = null;
    public AudioClip tunjoSound;

    private void Awake()
    {
        enemyAudioSource = GetComponent<AudioSource>();
    }
    

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();              
    }   
    
    public void SpawnBadsByArea(LevelArea area)
    {
        switch (area)
        {
            case LevelArea.Glitch:
                InvokeRepeating("SpawnGlitch", startDelay, repeatRateGlitch);
                break;

            case LevelArea.Snake:
                CancelInvoke("SpawnGlitch");
                InvokeRepeating("SpawnSnake", startDelay, repeatRateSnake);                
                break;

            case LevelArea.Tunjo:
                CancelInvoke("SpawnSnake");
                Invoke("SpawnTunjo", startDelay);
                break;
        }
    }   

    void SpawnGlitch()
    {
        if (!gameManager.gameOver)
        {
            Instantiate(glitchPrefab, spawnGlitchPos, glitchPrefab.transform.rotation);
            playGlitchSound = glitchSound;
            enemyAudioSource.clip = playGlitchSound;
            enemyAudioSource.Play();
        }        
    }

    void SpawnSnake()
    {
        if (!gameManager.gameOver)
        {
            Instantiate(snakePrefab, new Vector2(5, 2), Quaternion.identity);
            playSnakeSound = snakeSound;
            enemyAudioSource.clip = playSnakeSound;
            enemyAudioSource.Play();
        }
    }

    void SpawnTunjo()
    {
        Instantiate(tunjoPrefab, new Vector2(7, 0), Quaternion.identity);
        playTunjoSound = tunjoSound;
        enemyAudioSource.clip = playTunjoSound;
        enemyAudioSource.Play();
    }
}
