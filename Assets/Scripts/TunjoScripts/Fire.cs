using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private BirdController birdController;
    private UiManager uiManager;
    private Animator animator;

    [SerializeField]private float speed = 10;
    private float leftBound = -25;

    private void Start()
    {
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
      
        
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        

        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            birdController = other.GetComponent<BirdController>();

            birdController.live--;
            uiManager.LoseLife();
            animator.SetTrigger("FireImpact");
            

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

    private void MoveLeft() 
    {
        
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }

    }
}
