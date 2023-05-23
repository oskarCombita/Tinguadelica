using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private BirdController birdController; // Referencia al componente BirdController
    private UiManager uiManager; // Referencia al componente UiManager
    private Animator animator; // Referencia al componente Animator

    private void Start()
    {
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>(); // Obtiene una referencia al componente UiManager del objeto "Lives UI"
        animator = gameObject.GetComponent<Animator>(); // Obtiene una referencia al componente Animator adjunto a este objeto
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto colisionado tiene la etiqueta "Player"
        {
            birdController = other.GetComponent<BirdController>(); // Obtiene una referencia al componente BirdController del objeto colisionado

            birdController.live--; // Reduce en uno el valor de la variable "live" en el componente BirdController
            uiManager.LoseLife(); // Llama al método "LoseLife()" en el componente UiManager
            animator.SetTrigger("FireImpact"); // Activa el trigger "FireImpact" en el componente Animator

            birdController.StartBlinkColor(); // Llama al método "StartBlinkColor()" en el componente BirdController
            birdController.ShowVFXDamage(); // Llama al método "ShowVFXDamage()" en el componente BirdController

            if (uiManager.countMushrooms >= 1) // Comprueba si la variable "countMushrooms" en el componente UiManager es mayor o igual a 1
            {
                uiManager.countMushrooms--; // Reduce en uno el valor de la variable "countMushrooms" en el componente UiManager
                uiManager.UpdateMushroomUiCount(); // Llama al método "UpdateMushroomUiCount()" en el componente UiManager
                birdController.ShowVFXLoseMush(); // Llama al método "ShowVFXLoseMush()" en el componente BirdController
            }
        }
    }
}
