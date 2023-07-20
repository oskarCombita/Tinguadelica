using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class UiManager : MonoBehaviour
{
    public Image liveImage;
    public Sprite liveFull;
    public Sprite liveEmpty;

    public Image mushCountImage;
    public Sprite mushGlitch;
    public Sprite mushSnake;
    public Sprite mushTunjo;

    private BirdController birdController;
    private PlayerInput birdPlayerInput;
    private GameManager gameManager;

    public TextMeshProUGUI countMushroomsText;
    private RectTransform mushCanvasTransform;
    private float timeAnimMushCanvas = 0.4f;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiScoreText;
    [SerializeField] private TextMeshProUGUI hiScorePlayerText;

    public TextMeshProUGUI activeControlSchText;


    void Start()
    {
        birdController = GameObject.Find("Bird").GetComponent<BirdController>();
        birdPlayerInput = GameObject.Find("Bird").GetComponent<PlayerInput>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        mushCanvasTransform = GameObject.Find("Mushroom Canvas").GetComponent<RectTransform>();

        DrawHearts();

        mushCountImage.sprite = mushGlitch;

        UpdateMushroomUiCount();
    }

    private void Update()
    {
        activeControlSchText.text = birdPlayerInput.currentControlScheme.ToString();
    }

    public void DrawHearts()
    {
        for (int i = 0; i < birdController.maxLives; i++)
        {
            Image newLiveImage = Instantiate(liveImage, liveImage.transform.parent);
            newLiveImage.gameObject.SetActive(true);
            newLiveImage.sprite = liveFull;

            RectTransform rectTransform = newLiveImage.GetComponent<RectTransform>();
            rectTransform.anchoredPosition += new Vector2(i * -65, 0);
        }
        liveImage.gameObject.SetActive(false);
    }

    public void DrawFourHeart()
    {
        Image newLiveImage = Instantiate(liveImage, liveImage.transform.parent);
        newLiveImage.gameObject.SetActive(true);
        newLiveImage.sprite = liveFull;

        RectTransform rectTransform = newLiveImage.GetComponent<RectTransform>();
        rectTransform.anchoredPosition += new Vector2(3 * -65, 0);
    }

    public void AnimMushroomCanvas()
    {
        Vector2 smallSize = mushCanvasTransform.localScale;
        Vector2 bigSize = new Vector2(0.35f, 0.35f);
        LeanTween.scale(mushCanvasTransform, bigSize, timeAnimMushCanvas).setEase(LeanTweenType.easeInQuad)
            .setOnComplete(() =>
            {
                LeanTween.scale(mushCanvasTransform, smallSize, timeAnimMushCanvas).setEase(LeanTweenType.easeOutQuad);
            });
    }

    public void UpdateMushroomUiCount()
    {
        int number;
        if (gameManager.activeArea == LevelArea.Test)
        {
            number = 2;
        }
        else if(gameManager.activeArea != LevelArea.Tunjo)
        {
            number = gameManager.mushToChangeArea;
        }
        else
        {
            number = gameManager.mushToCompleteLevel;
        }
        countMushroomsText.text = birdController.pickedMush + " / " + number;
    }

    public void LoseLife()
    {     
        if (birdController.live >= 0)
        {
            Image[] lives = GetComponentsInChildren<Image>();
            lives[birdController.live].sprite = liveEmpty;
        }        
    }

    public void RecoverLife()
    {
        Image[] lives = GetComponentsInChildren<Image>();
        lives[birdController.live - 1].sprite = liveFull;        
    }

    public void RecoverFourLife()
    {
        Image[] lives = GetComponentsInChildren<Image>();
        foreach (Image lifeImage in lives)
        {
            lifeImage.sprite = liveFull;
        }
    }

    public void UpdateScoreCount()
    {
        scoreText.text = gameManager.score.ToString();
    }

    public void UpdateHiScoreCount(float hiScore)
    {
        hiScoreText.text = hiScore.ToString();
    }

    public void UpdateHiScorePlayer(string name)
    {
        hiScorePlayerText.text = name;
    }
}
