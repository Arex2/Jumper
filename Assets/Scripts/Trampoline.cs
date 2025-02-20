using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float jumpForce = 600f;

    //[SerializeField] private Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("Jump");
            collision.GetComponent<AudioSource>().PlayOneShot(collision.GetComponent<PlayerMovement>().hitEnemySound, 0.5f);
            collision.GetComponent<PlayerMovement>().PlayJumpParticles();
            //animator.SetTrigger("Jump");
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
            playerRb.AddForce(new Vector2(0, jumpForce));

        }
    }
}
