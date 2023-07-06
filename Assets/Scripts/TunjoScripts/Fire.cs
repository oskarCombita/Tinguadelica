using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private BirdController birdController; // Referencia al componente BirdController
    private UiManager uiManager; // Referencia al componente UiManager
    private Animator animator; // Referencia al componente Animator
    private GameManager gameManager;

    private AudioSource fireAudioSource;
    public AudioClip soundLossLife;
    //public AudioClip soundLossmushroom;

    private void Awake()
    {
        fireAudioSource = GetComponent<AudioSource>();
    }  

    private void Start()
    {
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>(); // Obtiene una referencia al componente UiManager del objeto "Lives UI"
        animator = gameObject.GetComponent<Animator>(); // Obtiene una referencia al componente Animator adjunto a este objeto
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto colisionado tiene la etiqueta "Player"
        {
            birdController = other.GetComponent<BirdController>(); // Obtiene una referencia al componente BirdController del objeto colisionado

            birdController.live--; // Reduce en uno el valor de la variable "live" en el componente BirdController
            uiManager.LoseLife(); // Llama al m�todo "LoseLife()" en el componente UiManager
            animator.SetTrigger("FireImpact"); // Activa el trigger "FireImpact" en el componente Animator

            gameManager.scoreBonus += -(gameManager.scoreIncrement * 2);

            fireAudioSource.PlayOneShot(soundLossLife, 0.8f);  //reproduccion perdida de una vida
            //glitchAudioSource.PlayOneShot(soundLossmushroom, 0.8f);

            birdController.StartBlinkColor(); // Llama al m�todo "StartBlinkColor()" en el componente BirdController
            birdController.ShowVFXDamage(); // Llama al m�todo "ShowVFXDamage()" en el componente BirdController

            if (birdController.pickedMush >= 1) // Comprueba si la variable "countMushrooms" en el componente UiManager es mayor o igual a 1
            {
                birdController.pickedMush--; // Reduce en uno el valor de la variable "countMushrooms" en el componente UiManager
                uiManager.UpdateMushroomUiCount(); // Llama al m�todo "UpdateMushroomUiCount()" en el componente UiManager
                birdController.ShowVFXLoseMush(); // Llama al m�todo "ShowVFXLoseMush()" en el componente BirdController
            }
        }
    }
}
