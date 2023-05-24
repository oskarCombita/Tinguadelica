using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DestroySnake : MonoBehaviour
{
    private Animator snakeAnimator;

    void Start()
    {
        snakeAnimator = GetComponent<Animator>();
        Destroy(gameObject, snakeAnimator.GetCurrentAnimatorStateInfo(0).length);
    }
}
