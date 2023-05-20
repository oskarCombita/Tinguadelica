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
        if (other.CompareTag("Player"))
        {            
            birdController = other.GetComponent<BirdController>();

            birdController.live--;
            uiManager.LoseLife();

            birdController.StartBlinkColor();
            birdController.ShowVFXDamage();

            if (uiManager.countMushrooms == 1)
            {
                uiManager.countMushrooms--;
                uiManager.UpdateMushroomUiCount();
                birdController.ShowVFXLoseMush();
            }
            else if (uiManager.countMushrooms >= 2)
            {
                uiManager.countMushrooms -= 2;
                uiManager.UpdateMushroomUiCount();
                birdController.ShowVFXLoseMush();
            }
        }        
    }
}
