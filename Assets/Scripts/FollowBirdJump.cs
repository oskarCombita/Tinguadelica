using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBirdJump : MonoBehaviour
{
    public GameObject bird;
    public float speedTransition = 5.0f;
    private Vector3 startPos = new Vector3();
    public float yBound;
    
    void Start()
    {
        startPos = transform.position;
    }

    
    void LateUpdate()
    {
        if (bird.transform.position.y > yBound)
        {
            Vector3 newPosition = new Vector3(transform.position.x, bird.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speedTransition);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * speedTransition);
        }
    }
}
