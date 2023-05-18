using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Transform[] pointsOfPosition;

    [SerializeField] private float minDistance;

    private int nextPoint = 0;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        //nextPoint = Random.Range(0, pointsOfPosition.Length);

        spriteRenderer = GetComponent<SpriteRenderer>();
        Flip();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, pointsOfPosition[nextPoint].position, speed * Time.deltaTime);
        
        if(Vector2.Distance(transform.position, pointsOfPosition[nextPoint].position) < minDistance)
        {
            //nextPoint = Random.Range(0, pointsOfPosition.Length);
            //Flip();

            nextPoint +=1;
            if(nextPoint >= pointsOfPosition.Length)
            {
             nextPoint = 0;
            }
            Flip();
        }
    }

    private void Flip()
    {
     if(transform.position.x < pointsOfPosition[nextPoint].position.x)
        {
            spriteRenderer.flipX = true;  
        }
        else 
        {
            spriteRenderer.flipX = false;
        } 

    }

}
