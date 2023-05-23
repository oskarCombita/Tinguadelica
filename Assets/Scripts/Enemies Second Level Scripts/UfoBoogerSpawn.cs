using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoBoogerSpawn : MonoBehaviour
{
    [SerializeField] public GameObject ufoBoogerPrefab;
    private GameManager gameManager;
    Vector2 spawnPos = Vector2.zero;
    private float startDelay = 3;
    private float repeatRate = 3;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        GameObject ufoBoogerInstance = Instantiate(ufoBoogerPrefab, new Vector2(19, 4), Quaternion.identity);
        StartCoroutine(DestroyAfterAnimation(ufoBoogerInstance));
    }
    

    private IEnumerator DestroyAfterAnimation(GameObject ufoBoogerInstance)
    {
        //if (!gameManager.gameOver){
        Animator animator = ufoBoogerInstance.GetComponent<Animator>();

        // Esperar hasta que la animaci�n termine
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length * 2);

        // Destruir el objeto "UfoBooger"
        Destroy(ufoBoogerInstance);
        //}
    }
}
