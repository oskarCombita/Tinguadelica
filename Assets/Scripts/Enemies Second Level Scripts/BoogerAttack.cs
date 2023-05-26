using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoogerAttack : MonoBehaviour
{
    private BirdController birdController;
    private UiManager uiManager;
    private GameManager gameManager;

    [SerializeField]private float speed = 4.3f;
    private float lowerBound = -1.7f;


    void Start()
    {
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Falling();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !gameManager.gameOver)
        {
            birdController = other.GetComponent<BirdController>();

            birdController.live--;
            uiManager.LoseLife();
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

    private void Falling() 
    {
        
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < lowerBound)
        {
            Destroy(gameObject);
        }

    }
}
