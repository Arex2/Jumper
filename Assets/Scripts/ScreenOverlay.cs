using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScreenOverlay : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, -0.35f, 0);
    private GameObject circle;
    [SerializeField] private float smoothing = 1.0f;
    [SerializeField] private Animator anim;



    void Start()
    {
        circle = this.transform.GetChild(0).gameObject;
        //anim = GetComponent<Animator>();
    }

    void LateUpdate()
    {

        Vector3 newPosition = target.position + offset;
        transform.position = newPosition;
        if (Input.GetKeyDown(KeyCode.X))
            anim.SetTrigger("awake");
        if (Input.GetKeyDown(KeyCode.C))
            anim.SetTrigger("death");
        //transform.position = new Vector3(newPosition.x, transform.position.y) + offset; //the camera will not move along the Y axis
        //transform.position = newPosition;
    }
    public void Gone()
    {
        anim.SetTrigger("respawn");
    }

    public void Shrink()
    {
        anim.SetTrigger("death");
        /*
        Vector3 newScale = new Vector3 (0, 0, 0);
        //tansform.GetChild(0).localScale = newScale;
        
        while (circle.transform.localScale.x >= newScale.x)
        {
            Vector3 nextScale = Vector3.Lerp(circle.transform.localScale, newScale, smoothing * Time.deltaTime);
            //transform.position = new Vector3(newPosition.x, newPosition.y) + offset;
            transform.GetChild(0).localScale = nextScale;
        }
        */

        //this.transform.GetChild(0).localScale = new Vector3(3,3,3);
    }

    public void Grow()
    {
        anim.SetTrigger("awake");
        /*
        Vector3 newScale = new Vector3(10, 10, 10);
        while (circle.transform.localScale.x <= newScale.x)
        {
            Vector3 nextScale = Vector3.Lerp(circle.transform.localScale, newScale, smoothing * Time.deltaTime);
            //transform.position = new Vector3(newPosition.x, newPosition.y) + offset;
            transform.GetChild(0).localScale = nextScale;
        }*/
    }

}
