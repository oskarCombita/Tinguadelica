using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunjoDeath : MonoBehaviour
{
    private UiManager uiManager;
    private GameManager gameManager;
    private Animator animator;
    public int deathNumber;

    private void Start()
    {
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Death();
    }

    public void Death()
    {
        if (uiManager.countMushrooms >= deathNumber)
        {
            animator.SetTrigger("DeathTrigger");
            Invoke("DestroyTunjo", 1f);
            //DestroyTunjo();
        }
    }

    private void DestroyTunjo()
    {
        Destroy(gameObject);
        Debug.Log("TunjoKill");
    }

}
