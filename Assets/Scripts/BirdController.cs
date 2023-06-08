using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BirdController : MonoBehaviour
{
    private Rigidbody2D birdRB;
    private Animator birdAnimator;

    private float jumpEnergy = 1f;
    private float timeEnergy = 0f;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = false;
    public bool isCatching = false;

    [HideInInspector] public int live;
    public int maxLives = 3;

    public int pickedMush;

    private bool catchMushroom;

    private UiManager uiManager;
    private GameManager gameManager;

    private float horizontalInput;
    [SerializeField] float speedX;
    [SerializeField] float xRange;

    [SerializeField] private Color damageColor;
    [SerializeField] private Color damageColor2;
    [SerializeField] private Color liveColor;
    [SerializeField] private Color mushColor;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;

    private AudioSource birdAudioSource;
    AudioClip playSound = null;
    public AudioClip bikeSound;
    public AudioClip jumpSound;
    public AudioClip fallSound;

    public AudioClip lifeSound;
    public AudioClip mushroomSound;
    public AudioClip yellowLifeSound;

    public Slider jumpEnergySlider;

    private void Awake()
    {
        live = maxLives;
        birdAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        birdAnimator = GetComponent<Animator>();
        birdRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        GameManager.originalGravity = Physics2D.gravity.y;
        Physics2D.gravity *= gravityModifier;
    }

    void Update()
    {
        birdAnimator.SetBool("onGround", isOnGround);
        birdAnimator.SetBool("catch", isCatching);

        if (!gameManager.gameOver)
        {
            CatchControl();
            JumpControl();
        }
        
        MoveControl();
    }

    void JumpControl()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeEnergy = 0f;
        }

        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            timeEnergy += Time.deltaTime;
            jumpEnergySlider.value = Mathf.Clamp01(timeEnergy);
        }

        if (Input.GetKeyUp(KeyCode.Space) && isOnGround)
        {
            SetJumpEnergy();

            jumpEnergySlider.value = 0f;

            birdRB.AddForce(Vector2.up * jumpForce * jumpEnergy, ForceMode2D.Impulse);
            isOnGround = false;
            isCatching = false;

            birdAudioSource.Stop();
            birdAudioSource.PlayOneShot(jumpSound, 0.6f);
        }
    }

    void CatchControl()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isCatching)
        {
            isCatching = true;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isCatching = false;
        }
    }

    void MoveControl()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * speedX * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            birdAudioSource.PlayOneShot(fallSound, 0.6f);
            Invoke("PlayBikeSound", 0.25f);
        }

        if (collision.gameObject.CompareTag("Hole") && !gameManager.gameOver)
        {
            gameManager.gameOver = true;
            gameManager.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mushroom") && isCatching && !gameManager.gameOver)
        {
            catchMushroom = true;
            StartCoroutine(AddMushDelay(0.3f, 1));
            Destroy(collision.gameObject, 0.3f);
            Invoke("ShowVFXCatch", 0.3f);
            spriteRenderer.color = mushColor;
            Invoke("ResetColor", 0.4f);
            Invoke("ResetCatchMush", 0.33f);
            birdAudioSource.PlayOneShot(mushroomSound, 1f);
        }

        if (collision.gameObject.CompareTag("FlyMushroom") && !gameManager.gameOver)
        {
            StartCoroutine(AddMushDelay(0.3f, 3));
            Destroy(collision.gameObject, 0.1f);
            Invoke("ShowVFXCatch", 0.3f);
            spriteRenderer.color = mushColor;
            Invoke("ResetColor", 0.4f);
            birdAudioSource.PlayOneShot(mushroomSound, 1f);
        }

        if (collision.gameObject.CompareTag("Live") && !gameManager.gameOver)
        {
            if (live < maxLives)
            {
                live++;
                uiManager.RecoverLife();
                Destroy(collision.gameObject, 0.1f);
                spriteRenderer.color = liveColor;
                Invoke("ResetColor", 0.4f);
                GameObject vfxLive = VFXManager.Instance.RequestVfxLive();
                birdAudioSource.PlayOneShot(lifeSound, 0.8f);
            }
        }

        if (collision.gameObject.CompareTag("FlyLive") && !gameManager.gameOver)
        {
            if (maxLives == 3)
            {
                live = 4;
                maxLives = 4;
                uiManager.DrawFourHeart();
                uiManager.RecoverFourLife();
                Destroy(collision.gameObject, 0.1f);
                spriteRenderer.color = liveColor;
                Invoke("ResetColor", 0.4f);
                GameObject vfxLifeX4 = VFXManager.Instance.RequestVfxLifeX4();
                birdAudioSource.PlayOneShot(yellowLifeSound, 0.8f);
            }
            else
            {
                if (live < maxLives)
                {
                    live = 4;
                    uiManager.RecoverFourLife();
                    Destroy(collision.gameObject, 0.1f);
                    spriteRenderer.color = liveColor;
                    Invoke("ResetColor", 0.4f);
                    GameObject vfxLifeX4 = VFXManager.Instance.RequestVfxLifeX4();
                    birdAudioSource.PlayOneShot(yellowLifeSound, 0.8f);
                }               
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mushroom") && isCatching && !catchMushroom)
        {
            uiManager.AnimMushroomCanvas();
            pickedMush++;
            uiManager.UpdateMushroomUiCount();
            Destroy(collision.gameObject, 0.1f);
            catchMushroom = true;
            Invoke("ShowVFXCatch", 0.1f);
            spriteRenderer.color = mushColor;
            Invoke("ResetColor", 0.4f);
            Invoke("ResetCatchMush", 0.13f);
            birdAudioSource.PlayOneShot(mushroomSound, 1f);
        }
    }

    void ResetCatchMush()
    {
        catchMushroom = false;
    }

    void ShowVFXCatch()
    {
        GameObject vfxCatch = VFXManager.Instance.RequestVfxCatch();
    }

    public void ShowVFXDamage()
    {
        GameObject vfxDamage = VFXManager.Instance.RequestVfxDamage();
    }

    public void ShowVFXLoseMush()
    {
        GameObject vfxLoseMush = VFXManager.Instance.RequestVfxLoseMush();
    }

    float SetJumpEnergy()
    {
        if (timeEnergy < 0.4f)
        {
            jumpEnergy = 1f;
        }
        else if (timeEnergy > 1.4f)
        {
            jumpEnergy = 2f;
        }
        else
        {
            jumpEnergy = 1f + (timeEnergy - 0.4f);
        }
        return jumpEnergy;
    }

    public void StartBlinkColor()
    {
        InvokeRepeating("BlinkColor", 0f, 0.2f);
        Invoke("StopBlinkColor", 0.6f);
    }

    public void BlinkColor()
    {
        if (spriteRenderer.color == damageColor)
        {
            spriteRenderer.color = damageColor2;
        }
        else
        {
            spriteRenderer.color = damageColor;
        }
    }

    void StopBlinkColor()
    {
        CancelInvoke("BlinkColor");
        spriteRenderer.color = originalColor;
    }

    public void LevelCompleteColor()
    {
        Color color1 = new Color32(255, 177, 0, 255);
        Color color2 = new Color32(255, 0, 255, 255);
        Color color3 = Color.cyan;

        if(spriteRenderer.color == color1)
        {
            spriteRenderer.color = color2;
        }
        else if(spriteRenderer.color == color2)
        {
            spriteRenderer.color = color3;
        }
        else
        {
            spriteRenderer.color = color1;
        }
    }
  

    IEnumerator AddMushDelay(float delay, int mushToAdd)
    {
        yield return new WaitForSeconds(delay);
        uiManager.AnimMushroomCanvas();
        pickedMush += mushToAdd;
        uiManager.UpdateMushroomUiCount();
    }

    public void ResetColor()
    {
        spriteRenderer.color = originalColor;
    }

    void PlayBikeSound()
    {
        playSound = bikeSound;
        birdAudioSource.clip = playSound;
        birdAudioSource.Play();
    }
}
