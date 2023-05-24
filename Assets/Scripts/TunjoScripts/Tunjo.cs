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
    

    private void Start()
    {
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>(); // Obtiene una referencia al componente UiManager del objeto "Lives UI"
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        birdController = GameObject.Find("Bird").GetComponent<BirdController>();// Obtiene una referencia al componente GameManager del objeto "Game Manager"
        animator = GetComponent<Animator>(); // Obtiene una referencia al componente Animator adjunto a este objeto
    }

    private void LateUpdate()
    {
        Death(); // Llama al método Death()
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto colisionado tiene la etiqueta "Player"
        {       
            birdController.live--; // Reduce en uno el valor de la variable "live" en el componente BirdController
            uiManager.LoseLife(); // Llama al método "LoseLife()" en el componente UiManager

            birdController.StartBlinkColor(); // Llama al método "StartBlinkColor()" en el componente BirdController
            birdController.ShowVFXDamage(); // Llama al método "ShowVFXDamage()" en el componente BirdController

            if (birdController.pickedMush == 1) // Comprueba si la variable "countMushrooms" en el componente UiManager es igual a 1
            {
                birdController.pickedMush--; // Reduce en uno el valor de la variable "countMushrooms" en el componente UiManager
                uiManager.UpdateMushroomUiCount(); // Llama al método "UpdateMushroomUiCount()" en el componente UiManager
                birdController.ShowVFXLoseMush(); // Llama al método "ShowVFXLoseMush()" en el componente BirdController
            }
            else if (birdController.pickedMush >= 2) // Comprueba si la variable "countMushrooms" en el componente UiManager es mayor o igual a 2
            {
                birdController.pickedMush -= 2; // Reduce en dos el valor de la variable "countMushrooms" en el componente UiManager
                uiManager.UpdateMushroomUiCount(); // Llama al método "UpdateMushroomUiCount()" en el componente UiManager
                birdController.ShowVFXLoseMush(); // Llama al método "ShowVFXLoseMush()" en el componente BirdController
            }
        }
    }

    private void InstantiateFire()
    {
        Instantiate(fire, transform.position, Quaternion.identity); // Instancia el objeto de juego "fire" en la posición actual del objeto con la rotación por defecto
    }

    public void Death()
    {
        if (birdController.pickedMush >= gameManager.mushToCompleteLevel) // Comprueba si la variable "countMushrooms" en el componente UiManager es mayor o igual al número de muertes "deathNumber"
        {
            animator.SetTrigger("DeathTrigger"); // Activa el trigger "DeathTrigger" en el componente Animator
            Destroy(gameObject, 1f);
        }        
    }

}
