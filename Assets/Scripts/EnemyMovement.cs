using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private bool useSmartEnemyMovement; //toggle on/off enemy checking for ground

    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float bounce = 100f;
    [SerializeField] private float knockbackForce = 400f;
    [SerializeField] private float upwardForce = 100f;
    [SerializeField] private int dmgGiven = 1;
    [SerializeField] private Transform infront;
    [SerializeField] private LayerMask whatIsGround;

    private bool canMove = true;

    private float rayDistance = 1f;

    private SpriteRenderer rend; //Behövs ju egentligen inte när mina enemies kollar frammåt//eller när de roteras 180
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(useSmartEnemyMovement)
        {
            if (!CheckIfGroundInfront()) //aka no ground in front of enemy
            {
                //flip enemy (from facing right to facing left etc)
                this.transform.Rotate(0, -180, 0);
            }
        }
        else
        {
            if (moveSpeed < 0f)
            {
                rend.flipX = false;
            }
            if (moveSpeed > 0f)
            {
                rend.flipX = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;
        transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBlock") || collision.gameObject.CompareTag("Enemy"))
        {
            if (useSmartEnemyMovement) //makes this work with smart enemy movement
                this.transform.Rotate(0, -180, 0);
            else
                moveSpeed = -moveSpeed;
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(dmgGiven);

            if(collision.transform.position.x > transform.position.x)//spelaren står till höger om enemy
            {
                collision.gameObject.GetComponent<PlayerMovement>().TakeKnockback(knockbackForce, upwardForce);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerMovement>().TakeKnockback(-knockbackForce, upwardForce);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.GetComponent<Rigidbody2D>().velocity.x, 0); //la till detta själv 
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bounce));
            GetComponent<Animator>().SetTrigger("Hit");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //AudioSource a = collision.GetComponent<AudioSource>();
            //Debug.Log("A: " + a.ToString());
            //AudioClip b = collision.GetComponent<PlayerMovement>().hitEnemySound;


            //Debug.Log(b.ToString());
            //a.PlayOneShot(b, 0.5f);
            collision.GetComponent<AudioSource>().pitch = 0.5f;
            collision.GetComponent<AudioSource>().PlayOneShot(collision.GetComponent<PlayerMovement>().hitEnemySound, 0.5f);
            collision.GetComponent<PlayerMovement>().PlayJumpParticles();
            canMove = false;
            Destroy(gameObject, 0.25f); //Imagine mario effect där sprite åker upp och sen ner i en båge av skärmen
        }
    }

    private bool CheckIfGroundInfront() //checks if there is ground in front of enemy
    {
        RaycastHit2D hit = Physics2D.Raycast(infront.position, Vector2.down, rayDistance, whatIsGround);

        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
