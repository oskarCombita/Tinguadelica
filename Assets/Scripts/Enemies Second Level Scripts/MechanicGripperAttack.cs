using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicGripperAttack : MonoBehaviour
{
    private BirdController birdController;
    private UiManager uiManager;
    private GameManager gameManager;

    private AudioSource GripperAudioSource;
    public AudioClip soundLossLife;
    //public AudioClip soundLossmushroom;

    private void Awake()
    {
        GripperAudioSource = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }  

    void Start()
    {
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !gameManager.gameOver)
        {
            birdController = other.GetComponent<BirdController>();

            birdController.live--;
            uiManager.LoseLife();

            GripperAudioSource.PlayOneShot(soundLossLife, 0.8f);
            //glitchAudioSource.PlayOneShot(soundLossmushroom, 0.8f);

            birdController.StartBlinkColor();
            birdController.ShowVFXDamage();
            if (birdController.pickedMush >= 1)
            {
                birdController.pickedMush--;
                uiManager.UpdateMushroomUiCount();
                birdController.ShowVFXLoseMush();
            }
        }
    }
}
