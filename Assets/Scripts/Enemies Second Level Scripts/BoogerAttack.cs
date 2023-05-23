using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoogerAttack : MonoBehaviour
{
    private BirdController birdController;
    private UiManager uiManager;
    [SerializeField]private float speed = 4.3f;
    private float lowerBound = -1.7f;


    void Start()
    {
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Falling();
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

            if (uiManager.countMushrooms >= 1)
            {
                uiManager.countMushrooms--;
                uiManager.UpdateMushroomUiCount();
                birdController.ShowVFXLoseMush();
            }
        }
    }

    private void Falling() 
    {
        
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < lowerBound)
        {
            Destroy(gameObject);
        }

    }
}
