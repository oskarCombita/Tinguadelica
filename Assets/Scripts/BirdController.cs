using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public int live;
    public int maxLives = 3;

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


    void Start()
    {
        uiManager = GameObject.Find("Lives UI").GetComponent<UiManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        birdRB = GetComponent<Rigidbody2D>();
        birdAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        GameManager.originalGravity = Physics2D.gravity.y;
        Physics2D.gravity *= gravityModifier;
    }

    void Update()
    {
        birdAnimator.SetBool("onGround", isOnGround);
        birdAnimator.SetBool("catch", isCatching);

        CatchControl();
        JumpControl();
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
        }

        if (Input.GetKeyUp(KeyCode.Space) && isOnGround)
        {
            Debug.Log("Time Energy" + timeEnergy);
            SetJumpEnergy();

            birdRB.AddForce(Vector2.up * jumpForce * jumpEnergy, ForceMode2D.Impulse);
            isOnGround = false;
            isCatching = false;
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
        }

        if (collision.gameObject.CompareTag("Shark"))
        {
            live--;
            Debug.Log("Live: " + live);
            uiManager.LoseLife();
            Destroy(collision.gameObject, 0.1f);
            Invoke("ShowVFXDamage", 0.1f);
            StartCoroutine(BlinkColor());
        }

        if (collision.gameObject.CompareTag("Hole"))
        {
            gameManager.gameOver = true;
            gameManager.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mushroom") && isCatching)
        {
            catchMushroom = true;
            uiManager.AnimMushroomCanvas();
            uiManager.countMushrooms++;
            uiManager.UpdateMushroomUiCount();
            Destroy(collision.gameObject, 0.3f);
            Invoke("ShowVFXCatch", 0.3f);
            spriteRenderer.color = mushColor;
            Invoke("ResetColor", 0.4f);
        }

        if (collision.gameObject.CompareTag("Live"))
        {
            if (live < maxLives)
            {
                live++;
                Debug.Log("Live: " + live);
                uiManager.RecoverLife();
                Destroy(collision.gameObject, 0.1f);
                spriteRenderer.color = liveColor;
                Invoke("ResetColor", 0.4f);
                GameObject vfxLive = VFXManager.Instance.RequestVfxLive();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mushroom") && isCatching && !catchMushroom)
        {
            uiManager.AnimMushroomCanvas();
            uiManager.countMushrooms++;
            uiManager.UpdateMushroomUiCount();
            Destroy(collision.gameObject, 0.1f);
            catchMushroom = true;
            Invoke("ShowVFXCatch", 0.1f);
            spriteRenderer.color = mushColor;
            Invoke("ResetColor", 0.4f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        catchMushroom = false;
    }

    void ShowVFXCatch()
    {
        GameObject vfxCatch = VFXManager.Instance.RequestVfxCatch();
        //Vector3 vfxPos = new Vector3(-0.5f, 0, 0);
        //vfxCatch.transform.position = transform.position + vfxPos;
    }

    public void ShowVFXDamage()
    {
        GameObject vfxDamage = VFXManager.Instance.RequestVfxDamage();
    }

    float SetJumpEnergy()
    {
        if (timeEnergy < 0.4f)
        {
            jumpEnergy = 1f;
            Debug.Log("Jump Energy" + jumpEnergy);
        }
        else if (timeEnergy > 1.4f)
        {
            jumpEnergy = 2f;
            Debug.Log("Jump Energy" + jumpEnergy);
        }
        else
        {
            jumpEnergy = 1f + (timeEnergy - 0.4f);
            Debug.Log("Jump Energy" + jumpEnergy);
        }
        return jumpEnergy;
    }

    public IEnumerator BlinkColor()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 0.6f)
        {
            if (spriteRenderer.color == damageColor)
            {
                spriteRenderer.color = damageColor2;
            }
            else
            {
                spriteRenderer.color = damageColor;
            }

            yield return new WaitForSeconds(0.2f);
            elapsedTime += 0.2f;
        }

        spriteRenderer.color = originalColor;
    }

    public void ResetColor()
    {
        spriteRenderer.color = originalColor;
    }
}
