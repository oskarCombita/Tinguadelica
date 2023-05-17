using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXDesactivate : MonoBehaviour
{
    [SerializeField] float delay;
    private BirdController birdController;

    private void Start()
    {
        birdController = GameObject.Find("Bird").GetComponent<BirdController>();
    }

    private void OnEnable()
    {
        Invoke("DesactivateVFX", delay);
    }

    private void Update()
    {
        Vector3 vfxPos = new Vector3(-0.5f, 0, 0);
        transform.position = birdController.gameObject.transform.position + vfxPos;
    }

    void DesactivateVFX()
    {
        gameObject.SetActive(false);
    }
}
