using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    //[SerializeField]
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        //do effect
        Effect();
    }

    private void Effect()
    {
        Debug.Log("Effekt!");
        //this.gameObject.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        if(anim != null)
        anim.SetTrigger("Appear");
    }
}
