using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAttack : MonoBehaviour
{
    private BirdController birdController;
    private UiManager uiManager;
    private GameManager gameManager;

    private AudioSource snakeAudioSource;
    public AudioClip soundLossLife; 
    public AudioClip soundLossmushroom;

    private void Awake()
    {
        snakeAudioSource = GetComponent<AudioSource>();
    }    

    private void Start()
    {
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !gameManager.gameOver)
        {            
            birdController = other.GetComponent<BirdController>();

            birdController.live--;
            uiManager.LoseLife();

            snakeAudioSource.PlayOneShot(soundLossLife, 0.8f);
            snakeAudioSource.PlayOneShot(soundLossmushroom, 0.8f);

            birdController.StartBlinkColor();
            birdController.ShowVFXDamage();

            if (birdController.pickedMush == 1)
            {
                birdController.pickedMush--;
                uiManager.UpdateMushroomUiCount();
                birdController.ShowVFXLoseMush();
            }
            else if (birdController.pickedMush >= 2)
            {
                birdController.pickedMush -= 2;
                uiManager.UpdateMushroomUiCount();
                birdController.ShowVFXLoseMush();
            }
        }        
    }
}
