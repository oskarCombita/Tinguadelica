using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowOnMobile : MonoBehaviour
{
    private PlayerInput playerInputBird;

    public string mobileControlScheme = "Mobile";
    void Start()
    {
        playerInputBird = GameObject.Find("Bird").GetComponent<PlayerInput>();

        gameObject.SetActive(Application.isMobilePlatform);

        if (Application.isMobilePlatform)
        {
            playerInputBird.SwitchCurrentControlScheme(mobileControlScheme, Keyboard.current);
        }
    }
}
