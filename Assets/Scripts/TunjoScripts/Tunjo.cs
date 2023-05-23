using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunjo : MonoBehaviour
{
    private BirdController birdController; // Referencia al componente BirdController
    private UiManager uiManager; // Referencia al componente UiManager
    [SerializeField] private GameObject fire; // Objeto de juego para instanciar fuego
    private Animator animator; // Referencia al componente Animator
    private GameManager gameManager; // Referencia al componente GameManager
    public int deathNumber; // N�mero de muertes

    private void Start()
    {
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>(); // Obtiene una referencia al componente UiManager del objeto "Lives UI"
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Obtiene una referencia al componente GameManager del objeto "Game Manager"
        animator = GetComponent<Animator>(); // Obtiene una referencia al componente Animator adjunto a este objeto
    }

    private void Update()
    {
        Death(); // Llama al m�todo Death()
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto colisionado tiene la etiqueta "Player"
        {
            birdController = other.GetComponent<BirdController>(); // Obtiene una referencia al componente BirdController del objeto colisionado

            birdController.live--; // Reduce en uno el valor de la variable "live" en el componente BirdController
            uiManager.LoseLife(); // Llama al m�todo "LoseLife()" en el componente UiManager

            birdController.StartBlinkColor(); // Llama al m�todo "StartBlinkColor()" en el componente BirdController
            birdController.ShowVFXDamage(); // Llama al m�todo "ShowVFXDamage()" en el componente BirdController

            if (uiManager.countMushrooms == 1) // Comprueba si la variable "countMushrooms" en el componente UiManager es igual a 1
            {
                uiManager.countMushrooms--; // Reduce en uno el valor de la variable "countMushrooms" en el componente UiManager
                uiManager.UpdateMushroomUiCount(); // Llama al m�todo "UpdateMushroomUiCount()" en el componente UiManager
                birdController.ShowVFXLoseMush(); // Llama al m�todo "ShowVFXLoseMush()" en el componente BirdController
            }
            else if (uiManager.countMushrooms >= 2) // Comprueba si la variable "countMushrooms" en el componente UiManager es mayor o igual a 2
            {
                uiManager.countMushrooms -= 2; // Reduce en dos el valor de la variable "countMushrooms" en el componente UiManager
                uiManager.UpdateMushroomUiCount(); // Llama al m�todo "UpdateMushroomUiCount()" en el componente UiManager
                birdController.ShowVFXLoseMush(); // Llama al m�todo "ShowVFXLoseMush()" en el componente BirdController
            }
        }
    }

    private void InstantiateFire()
    {
        Instantiate(fire, transform.position, Quaternion.identity); // Instancia el objeto de juego "fire" en la posici�n actual del objeto con la rotaci�n por defecto
    }

    public void Death()
    {
        if (uiManager.countMushrooms >= deathNumber) // Comprueba si la variable "countMushrooms" en el componente UiManager es mayor o igual al n�mero de muertes "deathNumber"
        {
            animator.SetTrigger("DeathTrigger"); // Activa el trigger "DeathTrigger" en el componente Animator
            Invoke("DestroyTunjo", 1f); // Invoca el m�todo "DestroyTunjo" despu�s de 1 segundo
        }
    }

    private void DestroyTunjo()
    {
        Destroy(gameObject); // Destruye el objeto al que est� adjunto este script
    }
}
