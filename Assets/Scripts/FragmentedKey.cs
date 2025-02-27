using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentedKey : MonoBehaviour
{
    [SerializeField]
    GameObject[] keyParts;
    bool hasBeenCollected;
    //int keyFragmentCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement.onGrounded += OnGroundTouch;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckAllKeyPartsPickedup() && !hasBeenCollected)
        {
            //form key (animation)
            //give player key that follows player or something
            Debug.Log("Key has formed");
            hasBeenCollected = true;
            OnCollected();
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
        transform.position = GameObject.Find("Player").transform.position;
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
