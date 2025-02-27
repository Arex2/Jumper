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
    //int keyFragmentCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement.onGrounded += OnGroundTouch;
    }
    bool temp = false;
    // Update is called once per frame
    void Update()
    {

        if(!temp)
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
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

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
    }

    private void OnGroundTouch() //TRIGGER SOMEHOW
    { 
        if(!hasBeenCollected)
        {
            SetKeyPartsActive();
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
}
