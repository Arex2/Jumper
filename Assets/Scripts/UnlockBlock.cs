using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockBlock : MonoBehaviour
{
    [SerializeField]
    public GameObject destroyParticles;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip unlockSound, destructionSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("BigKey"))
        {
            //check if key is activated
            if(collision.gameObject.GetComponent<SpriteRenderer>().isVisible)
            {
                Debug.Log("Happens");
                //destroy key
                Destroy(collision.gameObject);

                //unlock block
                //play unlock sound
                audioSource.PlayOneShot(unlockSound);

                //destroy block
                Invoke("DeleteBlock", 1.4f);
            }

        }
    }


    private void PlayDestroyParticles()
    {
        Instantiate(destroyParticles, transform.position, Quaternion.Euler(0,180,0));
    }

    private void DeleteBlock()
    {
        //play destruction sound
        audioSource.PlayOneShot(destructionSound);
        //delete this block
        Invoke("DestroyBlock", 0.3f);
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
        //play destruction particle effect
        PlayDestroyParticles();
    }


}
