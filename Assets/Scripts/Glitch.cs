using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitch : MonoBehaviour
{
    private BirdController birdController;
    private UiManager uiManager;
    private GameManager gameManager;

    private AudioSource glitchAudioSource;
    public AudioClip soundLossLife;
    //public AudioClip soundLossmushroom;

    private void Awake()
    {
        glitchAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        birdController = GameObject.Find("Bird").GetComponent<BirdController>();
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !gameManager.gameOver)
        {
            
            birdController.live--;
            uiManager.LoseLife();

            glitchAudioSource.PlayOneShot(soundLossLife, 0.8f);
            //glitchAudioSource.PlayOneShot(soundLossmushroom, 0.8f);

            birdController.StartBlinkColor();
            birdController.ShowVFXDamage();

            if (birdController.pickedMush >= 1)
            {
                birdController.pickedMush--;
                uiManager.UpdateMushroomUiCount();
                birdController.ShowVFXLoseMush();
            }            

            Destroy(gameObject, 0.1f);            
        }
    }       

}
