using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FragmentedKey : MonoBehaviour
{
    [SerializeField]
    GameObject[] keyParts;
    bool hasBeenCollected;
    //[SerializeField]
    //SpriteRenderer spriteR;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    GameObject player;

    [SerializeField]
    Transform target; //door
    public float speed = 0.1f;

    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip keyFormed, fragmentsDropped;
    //int keyFragmentCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement.onGrounded += OnGroundTouch;
    }
    bool temp = false;
    float variabel = 0f;
    // Update is called once per frame
    void Update()
    {
        variabel += Time.deltaTime * 3;
        var sinus = Mathf.Sin(5f + (variabel)) * 2; //för sinus våg
        var distance = Mathf.Abs(transform.position.x) - Mathf.Abs(target.position.x); //FÖRUTSÄTTER ATT DEN RÖR sig horizontellt
        //distance = distance / 1; //gör till mellan 0 och 1;
        sinus = sinus * distance;

        if (!temp)
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
        if (CheckAllKeyPartsPickedup() && !hasBeenCollected)
        {
            //form key (animation)
            //give player key that follows player or something
            Debug.Log("Key has formed");
            hasBeenCollected = true;
            OnCollected();
        }

        //efter animation är done move towards target door

        if(Input.GetKeyDown(KeyCode.V))
        {
            temp = true;
        }
        if(temp)
        {
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);  //Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y + sinus), step);
            transform.position = new Vector3(transform.position.x, transform.position.y + sinus/500);
            //Debug.Log("Sinusvåg: " + sinus + " variabel " + variabel + " distance: " + distance);



        }


        /*
        if (Input.GetKeyDown(KeyCode.T)) //fungerar
        {
            OnGroundTouch();
        }
        */
    }

    private void OnCollected()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().keyCount++; //INCREASES KEY COUNT BY ONE
        //spriteR.enabled = true;
        anim.SetTrigger("FormKey");
        //play sudio source
        //audioSource.Play();
        Invoke("PlaySound", 0.87f);
    }

    private void PlaySound()
    {
        //audioSource.pitch = Random.Range(1f, 1.3f);
        audioSource.volume = 0.4f;
        audioSource.PlayOneShot(keyFormed, 0.5f);
    }

    private void OnGroundTouch() //TRIGGER SOMEHOW
    {
        if (!hasBeenCollected)
        {
            if (CheckAnyPickedUp())
            {
                //BORDE EGENTLIGEN OCKSÅ SPELA ETT SOUND HÄR SOM REPRESENTERAR FAIl
                //audioSource.pitch = Random.Range(1f, 1.3f);
                audioSource.volume = 0.2f;
                audioSource.PlayOneShot(fragmentsDropped, 0.5f);
            }
            Invoke("SetKeyPartsActive", 0.2f);
            //SetKeyPartsActive();
        }
    }

    private void SetKeyPartsActive()
    {
        foreach (GameObject g in keyParts)
        {
            g.SetActive(true);
        }
    }

    private bool CheckAllKeyPartsPickedup()
    {
        foreach(GameObject g in keyParts)
        {
            if(g.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckAnyPickedUp()
    {
        foreach (GameObject g in keyParts)
        {
            if (!g.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }
}
