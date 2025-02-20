using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject box;

    private Animator anim;
    private bool hasPlayedAnimation = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !hasPlayedAnimation)
        {
            box.SetActive(false);
            hasPlayedAnimation=true;
            anim.SetTrigger("Move");
        }
    }
}
