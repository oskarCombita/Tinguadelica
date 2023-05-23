using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunjoDeath : MonoBehaviour
{
    private UiManager uiManager;
    private GameManager gameManager;
    private Animator animator;

    private void Start()
    {
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
    }

    public void Death()
    {
        if (uiManager.countMushrooms >= 1 && animator != null && !gameManager.gameOver)
        {
            animator.SetBool("TunjoDeath", true);
            Invoke("DestroyTunjo", 2f);
        }
    }

    private void DestroyTunjo()
    {
        Destroy(gameObject);
    }

}
