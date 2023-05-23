using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoBoogerAttack : MonoBehaviour
{
    private BirdController birdController;
    private UiManager uiManager;
    [SerializeField] private GameObject Yellowbooger;
    [SerializeField] private GameObject Greenbooger;

    void Start()
    {
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            birdController = other.GetComponent<BirdController>();

            birdController.live--;
            uiManager.LoseLife();

            birdController.StartBlinkColor();
            birdController.ShowVFXDamage();
            if (uiManager.countMushrooms >= 1)
            {
                uiManager.countMushrooms--;
                uiManager.UpdateMushroomUiCount();
                birdController.ShowVFXLoseMush();
            }
        }
    }

    private void InstantiateBoogerYellow()
    {
       Instantiate(Yellowbooger, transform.position, Quaternion.identity);
    }
    
    private void InstantiateBoogerGreen()
    {
       Instantiate(Greenbooger, transform.position, Quaternion.identity);
    }
}
