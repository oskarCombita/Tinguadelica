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
