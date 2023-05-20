using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAttack : MonoBehaviour
{
    private BirdController birdController;
    private UiManager uiManager;

    private void Start()
    {
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //birdController = GameObject.Find("Bird").GetComponent<BirdController>();
        if (other.CompareTag("Player"))
        {
            birdController = other.GetComponent<BirdController>();

            birdController.live--;
            uiManager.LoseLife();
            //Debug.Log("El jugador ha sido golpeado por Snake");
            //StartCoroutine(birdController.BlinkColor());
            birdController.StartBlinkColor();
            birdController.ShowVFXDamage();
            //Invoke("ShowVFXDamage", 0.1f);
        }
        if (uiManager.countMushrooms >= 1)
        {
            uiManager.countMushrooms--;
            uiManager.UpdateMushroomUiCount();
            birdController.ShowVFXLoseMush();
        }
        else if (uiManager.countMushrooms >= 2)
        {
            uiManager.countMushrooms -=2;
            uiManager.UpdateMushroomUiCount();
            birdController.ShowVFXLoseMush();
        }
    }
}
