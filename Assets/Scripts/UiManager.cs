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
    private SpawnSnake spawnSnake;

    public TextMeshProUGUI countMushroomsText;
    private RectTransform mushCanvasTransform;
    private float timeAnimMushCanvas = 0.4f;
    public int countMushrooms;
    public int mushToSpawnSnake;
    private bool hasSpawnSnake = false;


    void Start()
    {
        birdController = GameObject.Find("Bird").GetComponent<BirdController>();
        mushCanvasTransform = GameObject.Find("Mushroom Canvas").GetComponent<RectTransform>();
        spawnSnake = GameObject.Find("SpawnManager").GetComponent<SpawnSnake>();
        DrawHearts();

        countMushroomsText.text = "x " + countMushrooms;
    }

    private void Update()
    {
        SpawnSnake();
    }

    void SpawnSnake()
    {
        if (countMushrooms == mushToSpawnSnake && !hasSpawnSnake)
        {
            hasSpawnSnake = true;
            spawnSnake.InstantiateSnake();
        }
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
        if (countMushrooms >= 1)
        {
            countMushroomsText.text = "x " + countMushrooms;
        }
        else
        {
            countMushroomsText.text = "x 0";
        }
        
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
