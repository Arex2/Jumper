using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    FloorSwitchingBehaviour m_FloorSwitching;
    ScreenOverlay m_ScreenOverlay;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 150f;
    [SerializeField] private Transform footL, footR;
    [SerializeField] private Vector3Int spawnPos;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private AudioClip jumpSound, pickupSound, dmgSound, deathSound;
    [SerializeField] public AudioClip hitEnemySound;
    [SerializeField] public GameObject jumpParticles, itemParticles, deathParticles;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillColor;
    [SerializeField] private Color redHealth;
    [SerializeField] private TMP_Text coinText, deathsText, keysText;

    private float horizontalValue;
    private bool isGrounded;
    private float rayDistance = 0.25f;

    private bool canMove;

    private int startingHealth = 3;
    private int currentHealth = 0;

    public int keyCount;
    private int coinsCollected, deathCount;

    private bool gameActive;


    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    public AudioSource audioSource;

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    public bool GameActive
    { 
        get { return gameActive; }
        set { gameActive = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_FloorSwitching = GameObject.Find("Ground").GetComponent<FloorSwitchingBehaviour>();
        m_FloorSwitching.ResetFloor();

        m_ScreenOverlay = GameObject.Find("ScreenOverlay").GetComponent<ScreenOverlay>();
        m_ScreenOverlay.Grow();

        currentHealth = startingHealth;
        canMove = true;

        coinText.text = coinsCollected.ToString();
        deathsText.text = deathCount.ToString();
        keysText.text = keyCount.ToString();

        //coinText.text = "" + coinsCollected;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");

        if(horizontalValue < 0f) 
        {
            FlipSprite(true);
        }
        if(horizontalValue > 0f)
        {
            FlipSprite(false);
        }


        if(Input.GetButtonDown("Jump") && CheckIfGrounded())
        {

            //switchFloor
            //Invoke("SwitchFloor", 0.1f);
            m_FloorSwitching.SwitchFloor();
            Jump();
        }


        anim.SetFloat("MoveSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("VerticalSpeed", rb.velocity.y);
        anim.SetBool("IsGrounded", CheckIfGrounded());

    }

    /*
    private void SwitchFloor()
    {
        m_FloorSwitching.SwitchFloor();
    }
    */

    private void FixedUpdate()//"går på ett jämnt intervall 60 gånger i sekunden"
    {
        if(!canMove)
        {
            return;
        }
        rb.velocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            PlayItemParticles(collision);
            Destroy(collision.gameObject);
            coinsCollected++;
            coinText.text = coinsCollected.ToString();
            PlayPickupSound();
        }

        if(collision.CompareTag("Heart"))
        {
            RestoreHealth(collision.gameObject);
        }

        if (collision.CompareTag("Key"))
        {
            PlayItemParticles(collision);
            Destroy(collision.gameObject);
            keyCount++;
            keysText.text = keyCount.ToString();
            PlayPickupSound();
        }
    }

    private void PlayPickupSound()
    {
        audioSource.pitch = Random.Range(1f, 1.3f);
        audioSource.PlayOneShot(pickupSound, 0.5f);
    }

    private void FlipSprite(bool direction)
    {
        spriteRenderer.flipX = direction;
    }

    private void Jump()
    {
        //audioSource.pitch = 0.5f;
        //audioSource.PlayOneShot(jumpSound, 0.45f);
        rb.AddForce(new Vector2(0,jumpForce));
        //Instantiate(jumpParticles, transform.position, Quaternion.identity);
        PlayJumpParticles();
        FlashRed();

    }

    private void FlashRed()
    {
        spriteRenderer.color = Color.gray;
        Invoke("ReturnWhite", 0.2f);
    }

    private void ReturnWhite()
    {
        spriteRenderer.color = Color.white;
    }

    public void PlayJumpParticles()
    {
        Instantiate(jumpParticles, transform.position, Quaternion.identity);
    }

    public void PlayItemParticles(Collider2D other)
    {
        Instantiate(itemParticles, other.transform.position, Quaternion.identity);
    }


    public void TakeDamage(int dmgAmount)
    {
        currentHealth -= dmgAmount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            //DEATH
            Death();
            Invoke("Respawn", 0.75f);
            //Respawn();
            return; //så att dmgsound inte spelas när deathsound ska spelas
        }
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(dmgSound, 0.5f);
    }

    public void TakeKnockback(float knockbackForce, float upwardsForce)
    {
        if(currentHealth > 0) //only recieve knockback when player is alive (aka hp over 0)
        {
            canMove = false;
            rb.AddForce(new Vector2(knockbackForce, upwardsForce));
            Invoke("CanMoveAgain", 0.25f);
        }
    }

    private void CanMoveAgain()
    {
        if(currentHealth > 0) //kollar att hp är över 0, annars är det death state och då bör spelaren ej kunna gå
            canMove = true;
    }

    public void Death()
    {
        m_ScreenOverlay.Shrink();
        deathCount++;
        deathsText.text = deathCount.ToString();
        canMove = false;
        audioSource.pitch = 0.4f;
        audioSource.PlayOneShot(deathSound, 0.6f);
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        rb.velocity = Vector2.zero;
        spriteRenderer.enabled = false;
    }

    public void Respawn()
    {
        //egentligen vill man ju resetta scenen
        transform.position = spawnPos;
        m_ScreenOverlay.Gone();
        currentHealth = startingHealth;
        UpdateHealthBar();
        m_FloorSwitching.ResetFloor();
        canMove = true;
        spriteRenderer.enabled = true;

    }

    private void RestoreHealth(GameObject pickupItem)
    {
        if(currentHealth >= startingHealth)
        {
            return;
        }
        else
        {
            int healthToRestore = pickupItem.GetComponent<HealthPickup>().healthAmount;
            currentHealth += healthToRestore; // vill bara egentligen lägga till 1 health oavsätt (1 pickup = 1 health) ++; hade ju varit cleaner
            PlayPickupSound();
            UpdateHealthBar();
            PlayItemParticles(pickupItem.GetComponent<Collider2D>());
            Destroy(pickupItem);

            if(currentHealth >= startingHealth) // så att health aldrig går över max (also useless om jag har ba +1 health oavsätt alltid)
            {
                currentHealth = startingHealth;
            }
        }
    }

    private void UpdateHealthBar()
    {

        healthSlider.value = currentHealth;

        if(currentHealth >= 2)
        {
            fillColor.color = Color.white;
        }
        else
        {
            fillColor.color = redHealth;
        }
    }

    private bool CheckIfGrounded()
    {
        RaycastHit2D hitL = Physics2D.Raycast(footL.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D hitR = Physics2D.Raycast(footR.position, Vector2.down, rayDistance, whatIsGround);

        if (hitL.collider != null && hitL.collider.CompareTag("Ground") || hitR.collider != null && hitR.collider.CompareTag("Ground"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetSpawn(Vector3Int pos)
    {
        spawnPos = pos;
    }

}
