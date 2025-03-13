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

    bool unlocking;
    bool keyWaitTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("BigKey"))
        {

            if (keyWaitTimer)
            {
                //destroy key
                Destroy(collision.gameObject);
            }
            if (!unlocking)
            {

                if ("KeyFormed" == collision.gameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name) //GetCurrentAnimatorStateInfo(0).IsName("");
                {
                    unlocking = true;
                    //Debug.Log("Formed");
                    //Debug.Log("Happens");

                    Invoke("KeyWaitTimer", 0.2f);


                    //unlock block
                    //play unlock sound
                    audioSource.PlayOneShot(unlockSound);

                    //destroy block
                    Invoke("DeleteBlock", 1.4f);
                }


                //else
                //Debug.Log("not formed");
                /*
                //check if key is activated
                if(collision.gameObject.GetComponent<SpriteRenderer>().isVisible) // && ANIMATION IS DONE PLAYING
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
                */

            }
        }
    }

    private void KeyWaitTimer()
    {
        keyWaitTimer = true;
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
