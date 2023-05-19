using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnSnake : MonoBehaviour
{
    [SerializeField]private GameObject snake;
    //private BirdController birdController;
    private UiManager uiManager;

    void Start()
    {
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>();
        InstantiateSnake();
       
    }

    private void Update()
    {
       
    }

    public void InstantiateSnake()
    {

        ///Instantiate(snake, new Vector2(5, 2), Quaternion.identity);

        //if (uiManager.countMushrooms > 1)

            GameObject snakeInstance = Instantiate(snake, new Vector2(5, 2), Quaternion.identity);

            StartCoroutine(DestroyAfterAnimation(snakeInstance));
        
    }

    private IEnumerator DestroyAfterAnimation(GameObject snakeInstance)
    {
        Animator animator = snakeInstance.GetComponent<Animator>();

        // Esperar hasta que la animación termine
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length * 2);

        // Destruir el objeto "snakeInstance"
        Destroy(snakeInstance);
    }

}
