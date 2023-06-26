using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KinematicManager : MonoBehaviour
{
    public RawImage introVideo;
    public Image tinguadelicaLogo;
    public Button creditsBtn;
    public Button settingsBtn;
    public Button startBtn;
    private SpriteRenderer backgroundImage;

    private bool isFadingIn;

    public float timeOffIntro;
    public float fadeInDuration;
    private float fadeTimer = 0f;


    private void Awake()
    {
        introVideo.gameObject.SetActive(true);
    }

    void Start()
    {
        backgroundImage = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        Invoke("IntroOff", timeOffIntro);        
        Invoke("TinguadelicaOn", timeOffIntro + fadeInDuration);        
        Invoke("CreditsBtnOn", timeOffIntro + fadeInDuration + 0.2f);        
        Invoke("SettingsBtnOn", timeOffIntro + fadeInDuration + 0.4f);        
        Invoke("StartBtnOn", timeOffIntro + fadeInDuration + 0.6f);        
    }

    
    void Update()
    {
        ScreenOn();
    }

    void IntroOff()
    {
        introVideo.gameObject.SetActive(false);        
        isFadingIn = true;
    }

    void TinguadelicaOn()
    {
        tinguadelicaLogo.gameObject.SetActive(true);
    }

    void CreditsBtnOn()
    {
        creditsBtn.gameObject.SetActive(true);
    }

    void SettingsBtnOn()
    {
        settingsBtn.gameObject.SetActive(true);
    }

    void StartBtnOn()
    {
        startBtn.gameObject.SetActive(true);
    }

    void ScreenOn()
    {
        if (isFadingIn)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, fadeTimer / fadeInDuration);
            backgroundImage.color = new Color(255, 255, 255, alpha);

            if (fadeTimer >= fadeInDuration)
            {
                isFadingIn = false;
            }
        }
    }
}
