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
    }

    void LateUpdate()
    {

        Vector3 newPosition = target.position + offset;
        transform.position = newPosition;
        if (Input.GetKeyDown(KeyCode.X))
            anim.SetTrigger("awake");
        if (Input.GetKeyDown(KeyCode.C))
            anim.SetTrigger("death");
    }
    public void Gone()
    {
        anim.SetTrigger("respawn");
    }

    public void Shrink()
    {
        anim.SetTrigger("death");
    }

    public void Grow()
    {
        anim.SetTrigger("awake");
    }

}
