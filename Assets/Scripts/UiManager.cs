using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public Image liveImage;
    public Sprite liveFull;
    public Sprite liveEmpty;
    private BirdController birdController;
    private GameManager gameManager;

    public TextMeshProUGUI countMushroomsText;
    private RectTransform mushCanvasTransform;
    private float timeAnimMushCanvas = 0.4f;


    void Start()
    {
        birdController = GameObject.Find("Bird").GetComponent<BirdController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        mushCanvasTransform = GameObject.Find("Mushroom Canvas").GetComponent<RectTransform>();

        DrawHearts();

        UpdateMushroomUiCount();
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
}
